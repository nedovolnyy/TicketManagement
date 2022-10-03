import { Fragment } from 'react'
import './EventSeat.css'
import { useTranslation } from 'react-i18next'
import useAuth from '../hooks/useAuth'
import { useLocation, useNavigate } from 'react-router-dom'
import { EventSeatManagementApi } from '../api/EventsManagementApi'
import { UsersManagementApi } from '../api/UsersManagementApi'
import { EventsManagementApiHTTPSconfig, UsersManagementApiHTTPSconfig } from '../configurations/httpsConf'

const EventSeat = ({ eventSeat, eventAreaPrice }) => {
  const { auth } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const { t } = useTranslation();

  const handleSubmit = (event) => {
    event.preventDefault();

    const conf = window.confirm(t('Are you sure you want to buy this seat?'));
    if (auth?.userResponse.balance < eventAreaPrice)
      (navigate('/NoBalance'))
    else {
      if (conf) {
        const EventSeatClient = new EventSeatManagementApi(EventsManagementApiHTTPSconfig);
        EventSeatClient.apiEventSeatManagementEventSeatStatusEventSeatIdPost(eventSeat.id,
          {
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(auth?.accessToken) },
            withCredentials: true
          })
          .catch((error) => {
            console.log(error);
            alert(error);
          });

        const UserClient = new UsersManagementApi(UsersManagementApiHTTPSconfig);
        UserClient.apiUsersManagementPurchasePost(
          eventSeat.id,
          '~/Event?eventId='.concat(location.state?.event?.id, '+ seat:', 'r.', eventSeat.row, ';n.', eventSeat.number),
          eventAreaPrice,
          auth?.userResponse?.id,
          {
            headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(auth?.accessToken) },
            withCredentials: true
          })
          .catch((error) => {
            console.log(error);
            alert(error);
          });
        navigate('/Event',{ state: { event: location.state?.event, seatsAvailableCount: location.state?.seatsAvailableCount-1, seatsCount: location.state?.seatsCount } });
        // ToDo: navigate(0) with update auth
      }
    }
  }

  return (
    (auth?.roles)
      ? (<Fragment>
        <form key={"seat".concat(eventSeat.id)} /* Event/Purchase:{eventSeat.Id, returnUrl,Model.Price"*/ className="form-horizontal">
          <button key={"btn_".concat(eventSeat.id)} className="btn btn-outline-secondary btn-lg btn-block btn_style" type="button" onClick={(event) => handleSubmit(event)} disabled={(eventSeat.state !== 0) ? 'disabled' : null} >
            <label key={"lblRow_".concat(eventSeat.id)} className="text-sm-start">{eventSeat.row}</label>
            {eventAreaPrice}
            <label key={"lblCol_".concat(eventSeat.id)} className="text-sm-end">{eventSeat.number}</label>
          </button>
        </form>
      </Fragment>)
      : (<Fragment>
        <button key={"btn_".concat(eventSeat.id)} className="btn btn-outline-secondary btn-lg btn-block btn_style" type="button" disabled={(eventSeat.state !== 0) ? 'disabled' : null} >
          <label key={"lblRow_".concat(eventSeat.id)} className="text-sm-start">{eventSeat.row}</label>
          {eventAreaPrice}
          <label key={"lblCol_".concat(eventSeat.id)} className="text-sm-end">{eventSeat.number}</label>
        </button>
      </Fragment>)
  );
}

export default EventSeat;
