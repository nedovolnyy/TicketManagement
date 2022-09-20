import './SelectEventSeatPartial.css'
import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import { EventSeatManagementApi } from '../api/EventsManagementAPI'
import { configHTTPS } from '../configurations/httpsConf'
import { Seat } from './Seat'

class SelectEventSeatPartialPlain extends Component {
  static displayName = SelectEventSeatPartialPlain.name;

  constructor(props) {
    super(props);
    this.state = { eventSeats: [], loading: true };
  }

  componentDidMount() {
    this.getEventSeats();
  }

  async getEventSeats() {
    const EventSeatClient = new EventSeatManagementApi(configHTTPS);
    await EventSeatClient.apiEventSeatManagementEventSeatsByEventAreaIdEventAreaIdGet(this.props.eventArea.id)
      .then(result => this.setState({ eventSeats: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderIndexPage(eventSeats, eventArea, that) {
    var tempRow, tempNumber = 0;
    return (
      <div className="input-group" key={"eventArea".concat(eventArea.id)} >
        {eventSeats.map(seat => {
          return (
            (seat.row !== tempRow) ? (
              <Fragment>
                {(tempNumber > seat.number) ? (<></>) : (<div className='container row' key={"ContRow".concat(seat.row, seat.number)}></div>)}
                <div className='container col' key={"ContCol".concat(seat.row, seat.number)}>
                    <Seat seat={seat} eventAreaPrice={eventArea.price}>{tempRow = seat.row} {tempNumber = seat.number}</Seat>
                </div>
              </Fragment>) :
              (
                <Fragment>
                  <div className='container col' key={"ContCol".concat(seat.row, seat.number)} >
                    <Seat seat={seat} eventAreaPrice={eventArea.price}>{tempRow = seat.row}</Seat>
                  </div>
                </Fragment>)
          );
        }
        )}
      </div>
    );
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : SelectEventSeatPartialPlain.renderIndexPage(this.state.eventSeats, this.props.eventArea, this);

    return (
      <>{contents}</>
    );
  }
}

export const SelectEventSeatPartial = withTranslation()(SelectEventSeatPartialPlain);
