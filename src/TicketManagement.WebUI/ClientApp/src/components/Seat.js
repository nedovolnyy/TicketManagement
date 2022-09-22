import { Fragment } from 'react'
import './Seat.css'
import { useTranslation } from 'react-i18next'
import useAuth from '../hooks/useAuth'

export const Seat = ({ seat, eventAreaPrice }) => {
  const { auth } = useAuth();
  const { t } = useTranslation();

  return (
    (auth?.roles)
      ? (<Fragment>
        <form key={"seat".concat(seat.Id)} /* Event/Purchase:{seat.Id, returnUrl,Model.Price"*/ method="post" className="form-horizontal">
          <button key={"btn_".concat(seat.Id)} className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" onClick={() => window.confirm(t('Are you sure you want to buy this seat?'))} disabled={(seat.state !== 0) ? 'disabled' : null} >
            <label key={"lblRow_".concat(seat.Id)} className="text-sm-start">{seat.row}</label>
            {eventAreaPrice}
            <label key={"lblCol_".concat(seat.Id)} className="text-sm-end">{seat.number}</label>
          </button>
        </form>
      </Fragment>)
      : (<Fragment>
        <button key={"btn_".concat(seat.Id)} className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" disabled={(seat.state !== 0) ? 'disabled' : null} >
          <label key={"lblRow_".concat(seat.Id)} className="text-sm-start">{seat.row}</label>
          {eventAreaPrice}
          <label key={"lblCol_".concat(seat.Id)} className="text-sm-end">{seat.number}</label>
        </button>
      </Fragment>)
  );
}
