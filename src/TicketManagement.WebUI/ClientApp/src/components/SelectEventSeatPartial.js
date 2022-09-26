import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import { EventSeatManagementApi } from '../api/EventsManagementApi'
import { EventsManagementApiHTTPSconfig } from '../configurations/httpsConf'
import EventSeat from './EventSeat'

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
    const EventSeatClient = new EventSeatManagementApi(EventsManagementApiHTTPSconfig);
    await EventSeatClient.apiEventSeatManagementEventSeatsByEventAreaIdEventAreaIdGet(this.props.eventArea.id)
      .then(result => this.setState({ eventSeats: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderContent(eventSeats, eventArea, that) {
    var tempRow, tempNumber = 0;
    return (
      <div className="input-group" key={"eventArea".concat(eventArea.id)} >
        {eventSeats.map(eventSeat => {
          return (
            (eventSeat.row !== tempRow) ? (
              <Fragment key={"fr1".concat(eventSeat.id)}>
                {(tempNumber > eventSeat.number) ? (<></>) : (<div className='container row' key={"ContRow".concat(eventSeat.row, eventSeat.number)}></div>)}
                <div className='col-1' key={"ContCol".concat(eventSeat.row, eventSeat.number)}>
                  <EventSeat eventSeat={eventSeat} eventAreaPrice={eventArea.price}>{tempRow = eventSeat.row} {tempNumber = eventSeat.number}</EventSeat>
                </div>
              </Fragment>) :
              (
                <Fragment key={"fr2".concat(eventSeat.id)}>
                  <div className='col-1' key={"ContCol".concat(eventSeat.row, eventSeat.number)} >
                    <EventSeat eventSeat={eventSeat} eventAreaPrice={eventArea.price}>{tempRow = eventSeat.row}</EventSeat>
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
      : SelectEventSeatPartialPlain.renderContent(this.state.eventSeats, this.props.eventArea, this);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const SelectEventSeatPartial = withTranslation()(SelectEventSeatPartialPlain);
