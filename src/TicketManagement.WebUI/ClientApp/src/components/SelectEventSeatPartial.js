import React, { useEffect, useState } from "react"
import { withTranslation } from "react-i18next"
import './SelectEventSeatPartial.css'
import useAuth from '../hooks/useAuth';
import { EventSeatManagementApi } from '../api/EventsManagementAPI'
import { configHTTPS } from '../configurations/httpsConf'
import { ROLES } from "../App";

function SelectEventSeatPartialPlain(props) {
  const { t } = props;
  const { auth } = useAuth();
  const EventSeatClient = new EventSeatManagementApi(configHTTPS);
  const [eventSeatsCollection, setEventSeatsCollection] = useState([]);

  useEffect(() => {
    (() => {
      EventSeatClient.apiEventSeatManagementEventSeatsByEventAreaIdEventAreaIdGet(props.eventArea.id)
        .then(result =>
          setEventSeatsCollection(result));
    })();
  });

  //let maxRow = Math.max(...eventSeatsCollection.map(eventSeat => eventSeat.row));
  //let maxNumber = Math.max(...eventSeatsCollection.map(eventSeat => eventSeat.number));

  let tempRow = 0;
  /*
    var returnUrl = string.IsNullOrEmpty(Context.Request.Path) ? "~/" : $"~{Context.Request.Path.Value}" + Context.Request.QueryString.ToString();
  */

  function rowOdd(seat) {
    auth?.roles?.find(role => [ROLES.Administrator, ROLES.EventManager, ROLES.User]?.includes(role))
      ? (<form key={"seat".concat("seat.Id")} /* Event/Purchase:{seat.Id, returnUrl,Model.Price"*/
        method="post" className="form-horizontal">
        <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" onClick={() => window.confirm(t('Are you sure you want to buy this seat?'))} key={"btn".concat("seat.Id")} {...(seat.State !== 0) ? "disabled" : ""}>
          <label className="text-sm-start">{seat.row}</label>
          <label className="text-sm-start">{props.eventArea.price}</label>
          <label className="text-sm-end">{seat.number}</label>
        </button>
      </form>
      ) : (
        <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" key={"btn".concat("seat.Id")} {...(seat.State !== 0) ? "disabled" : ""}>
          <label className="text-sm-start">{seat.row}</label>
          <label className="text-sm-start">{props.eventArea.price}</label>
          <label className="text-sm-end">{seat.number}</label>
        </button>
      );
  }

  function rowNoOdd(seat) {
    auth?.roles?.find(role => [ROLES.Administrator, ROLES.EventManager, ROLES.User]?.includes(role))
      ? (<form key={"seat".concat("seat.Id")} /* Event/Purchase:{seat.Id, returnUrl,Model.Price"*/
        method="post" className="form-horizontal">
        <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" onClick={() => window.confirm(t('Are you sure you want to buy this seat?'))} key={"btn".concat("seat.Id")} {...(seat.State !== 0) ? "disabled" : ""} >
          <label className="text-sm-start">{seat.row}</label>
          <label className="text-sm-start">{props.eventArea.price}</label>
          <label className="text-sm-end">{seat.number}</label>
        </button>
      </form>
      ) : (
        <button className="btn btn-outline-secondary btn-lg btn-block btn_style" type="submit" key={"btn".concat("seat.Id")} {...(seat.State !== 0) ? "disabled" : ""} >
          <label className="text-sm-start">{seat.row}</label>
          <label className="text-sm-start">{props.eventArea.price}</label>
          <label className="text-sm-end">{seat.number}</label>
        </button>
      );
  }

  return (
    <table>
      <tbody>
        <div className="input-group">
          {(tempRow = 0)}
          {(console.log(tempRow))}
          {eventSeatsCollection.map(seat => (
            (seat.row !== tempRow) ? (
              <tr>
                <td>
                  <div className="container">
                    {rowOdd(seat)}
                  </div>
                </td>
              </tr>) : false // tr X
                (seat.row === tempRow) ? (
                  <td>
                    <div className="container">
                      {rowNoOdd(seat)}
                    </div>
                  </td>
          (tempRow = seat.row)
              // tr
              (console.log(tempRow))
          ):false
          ))}
        </div>
      </tbody>
    </table>
  );
}

export const SelectEventSeatPartial = withTranslation()(SelectEventSeatPartialPlain);
