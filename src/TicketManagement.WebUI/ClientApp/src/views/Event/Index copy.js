import React, { useEffect, useState } from "react"
import { withTranslation } from "react-i18next"
import { useLocation } from 'react-router-dom'
import { FormatDateTime } from '../../helpers/FormatDateTime'
import './Index.css'
import { EventAreaManagementApi } from '../../api/EventsManagementAPI'
import { configHTTPS } from '../../configurations/httpsConf'
import { SelectEventSeatPartial } from './SelectEventSeatPartial'

function EventPlain(props) {
  const { t } = props;
  const [eventAreas, setEventAreas] = useState([]);
  const [loading, setLoading] = useState(true);
  const { event, seatsCount, seatsAvailableCount } = props;

  useEffect(() => {
    if (loading) {
      (() => {
        const EventAreaClient = new EventAreaManagementApi(configHTTPS);
        EventAreaClient.apiEventAreaManagementEventAreasByEventIdEventIdGet(event.id)
          .then(result => setEventAreas(result))
          .catch((error) => {
            console.log(error);
            alert(error);
          })
      })();
      setLoading(false);
    };
  },[loading]);

  function renderSeatsSection() {
    console.log(loading);
    setLoading(true);
    console.log(eventAreas);
    return (
      <div className='container'>
        <div className='row'>
          {eventAreas.map(eventArea => (
            <tr>
              <td>
                <div className="text">
                  <div className="container">
                    <div className="container text-right">
                      <SelectEventSeatPartial eventArea={eventArea} />
                    </div>
                  </div>
                  <div className="right">
                    <p>{eventArea.description}</p>
                  </div>
                </div>
                <div className="textE">
                  <div className="right">
                    <p className="eventTime">{FormatDateTime(event.eventTime)}</p>
                  </div>
                </div>
              </td>
            </tr>
          ))}
        </div>
      </div>
    );
  }

  let contents = loading
    ? <p><em>{t('Loading...')}</em></p>
    : renderSeatsSection(eventAreas);

  return (
    <>
      <div className="text-center">
        <h1 className="display-4">{t('Welcome to')} {event.name}</h1>
        <div className="img__containerE">
          <img className="image shadow-lg" alt="" src={event.eventLogoImage} />
          <div className="img__descriptionE">
            <div className="img__headerE"><h2 className="noneDecoration">{event.name}</h2></div>
            <div className="text">
              <div className="right">
                <p>{seatsAvailableCount} / {seatsCount}</p>
              </div>
            </div>
            <div className="text">
              <div className="right">
                <p className="eventTime">{t('Avalaible')}</p>
              </div>
            </div>
          </div>
        </div>
        <p>{event.description}</p>
      </div>

      <div className="row">
        <table className="table">
          <tbody>
            {contents}
          </tbody>
        </table>
      </div>
    </>
  )
}

export const EventQ = withTranslation()(EventPlain);

export const Event = (props) => {
  const location = useLocation();

  return <EventQ {...props} event={location.state.event} seatsCount={location.state.seatsCount} seatsAvailableCount={location.state.seatsAvailableCount} />;
}
