import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import RequireRole from '../helpers/RequireRole'
import useAuth from '../hooks/useAuth'
import moment from 'moment'
import 'moment-timezone'
import './EventImg.css'
import { ROLES } from '../App'
import { Configuration, EventManagementApi } from '../api/EventsManagementAPI'
import { useNavigate } from 'react-router-dom'
import { EventManagementButton } from './EventManagementButton'

export default function EventImg(props) {
  const { t } = useTranslation();
  const [seatsAvailableCount, setSeatsAvailableCount] = useState();
  const navigate = useNavigate();
  const [seatsCount, setSeatsCount] = useState();

  useEffect(() => {
    (async () => {
      const urlForAvailableSeats = 'https://localhost:5003/api/EventManagement/SeatsAvailableCount/' + props.event.id;
      try {
        await fetch(urlForAvailableSeats, {
          method: 'GET'
        })
          .then(response => response.json())
          .then(seatsAvailableCount =>
            setSeatsAvailableCount(seatsAvailableCount));
      }
      catch (error) {
        console.log(error);
      }
    })();
  }, [props]);

  useEffect(() => {
    (async function () {
      const urlForSeatsCount = 'https://localhost:5003/api/EventManagement/SeatsCount/' + props.event.layoutId;
      try {
        await fetch(urlForSeatsCount, {
          method: 'GET'
        })
          .then(response => response.json())
          .then(seatsCount =>
            setSeatsCount(seatsCount));
      }
      catch (error) {
        console.log(error);
      }
    })();
  }, [props]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      navigate('Event')
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <div className='col' key={'col' + props.event.id}>
      <form onSubmit={handleSubmit} method="post" className="form-horizontal" role="form">
        <button className='invisible' type='submit' key={'submit' + props.event.id}>
          <div className='img__container visible' key={'img__container' + props.event.id}>
            <img className='image shadow-lg' alt='' src={props.event.eventLogoImage} key={'image' + props.event.id} />
            <div className='img__description' key={'img__description' + props.event.id}>
              <div className='text' key={'text1_' + props.event.id}>
                <div className='img__header right eventTime' key={'img__headerR' + props.event.id}>
                  {moment(props.event.eventTime).format('h:mm A M/d/yyyy')}
                </div>
                <div className='img__header left noneDecoration' key={'img__headerL' + props.event.id}>
                  <h2 key={'h2' + props.event.id}>{props.event.name}</h2>
                </div>
              </div>
              <div className='text' key={'text2_' + props.event.id}>
                <div className='left' key={'leftText' + props.event.id}>
                  <p key={'p1' + props.event.id}>{props.event.description.substring(0, Math.min(props.event.description.length, 45)) + '...'}</p>
                </div>
                <div className='right' key={'rightText' + props.event.id}>
                  <p key={'p2' + props.event.id}>{seatsAvailableCount}/{seatsCount}</p>
                </div>
              </div>
              <div className='text' key={'text3_' + props.event.id}>
                <div className='right eventTime' key={'rightAv' + props.event.id}>
                  <p key={'p3' + props.event.id}>{t('Avalaible')}</p>
                </div>
              </div>
            </div>
          </div>
        </button>
      </form>
      <EventManagementButton eventId={props.event.id} key={'EventManagementButton' + props.event.id} />
    </div >
  )
}
