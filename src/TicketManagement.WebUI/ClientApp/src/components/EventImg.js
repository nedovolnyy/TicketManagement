import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { FormatDateTime } from '../helpers/FormatDateTime'
import './EventImg.css'
import { EventManagementApi } from '../api/EventsManagementAPI'
import { useNavigate } from 'react-router-dom'
import { EventManagementButton } from './EventManagementButton'
import { configHTTPS } from '../configurations/httpsConf'

export default function EventImg(props) {
  const { t } = useTranslation();
  const EventClient = new EventManagementApi(configHTTPS);
  const [seatsAvailableCount, setSeatsAvailableCount] = useState();
  const navigate = useNavigate();
  const [seatsCount, setSeatsCount] = useState();

  useEffect(() => {
    (() => {
      EventClient.apiEventManagementSeatsAvailableCountEventIdGet(props.event.id).then(result =>
        setSeatsAvailableCount(result));
    })();
  });

  useEffect(() => {
    (() => {
      EventClient.apiEventManagementSeatsCountLayoutIdGet(props.event.layoutId).then(result =>
        setSeatsCount(result));
    })();
  });

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      navigate('Event', { state: { event: props.event, seatsAvailableCount: seatsAvailableCount, seatsCount: seatsCount } })
    } catch (error) {
      console.log(error);
    }
  }

  return (
    <div className='col' key={'col' + props.event.id}>
      <form onSubmit={handleSubmit} method="post" className="form-horizontal">
        <button className='invisible' type='submit' key={'submit' + props.event.id}>
          <div className='img__container visible' key={'img__container' + props.event.id}>
            <img className='image shadow-lg' alt='' src={props.event.eventLogoImage} key={'image' + props.event.id} />
            <div className='img__description' key={'img__description' + props.event.id}>
              <div className='text' key={'text1_' + props.event.id}>
                <div className='img__header right eventTime' key={'img__headerR' + props.event.id}>
                  {FormatDateTime(props.event.eventTime)}
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
