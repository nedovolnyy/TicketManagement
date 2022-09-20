import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import { useLocation } from 'react-router-dom'
import './Index.css'
import { EventAreaManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { SelectEventSeatPartial } from '../../components/SelectEventSeatPartial'
import { FormatDateTime } from '../../helpers/FormatDateTime'

class EventPlain extends Component {
  static displayName = EventPlain.name;

  constructor(props) {
    super(props);
    this.state = { eventAreas: [], loading: true };
    this.eventId = this.props.event.id;
  }

  componentDidMount() {
    this.getEventAreas();
  }

  async getEventAreas() {
    const EventAreaClient = new EventAreaManagementApi(EventsManagementApiHTTPSconfig);
    await EventAreaClient.apiEventAreaManagementEventAreasByEventIdEventIdGet(this.eventId)
      .then(result => this.setState({ eventAreas: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderContent(eventAreas, eventTime) {
    return (
      <Fragment>
        {eventAreas.map(eventArea => (
          <Fragment key={"fr".concat(eventArea.id)}>
            <div className="text" key={"SelectEventSeatPartial".concat(eventArea.id)}>
              <div className="container" key={"containerEA".concat(eventArea.id)} >
                <div className="container text-right" key={"containerEAtr".concat(eventArea.id)}>
                  <SelectEventSeatPartial eventArea={eventArea} key={"SelectEventSeatPartial".concat(eventArea.id)} />
                </div>
              </div>
              <div className="right" key={"containerEAr".concat(eventArea.id)}>
                <p>{eventArea.description}</p>
              </div>
            </div>
            <div className="textE" key={"containerEAte".concat(eventArea.id)}>
              <div className="right" key={"containerEArr".concat(eventArea.id)}>
                <p className="eventTime" key={"containerP".concat(eventArea.id)}>{FormatDateTime(eventTime)}</p>
              </div>
            </div>
          </Fragment>
        ))}
      </Fragment>
    );
  }

  render() {
    const { t } = this.props;
    const event = this.props.event;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : EventPlain.renderContent(this.state.eventAreas, event.eventTime)
    return (
      <Fragment>
        <div className="text-center">
          <h1 className="display-4">{t('Welcome to')} {event.name}</h1>
          <div className="img__containerE">
            <img className="image shadow-lg" alt="" src={event.eventLogoImage} />
            <div className="img__descriptionE">
              <div className="img__headerE"><h2 className="noneDecoration">{event.name}</h2></div>
              <div className="text">
                <div className="right">
                  <p>{this.props.seatsAvailableCount} / {this.props.seatsCount}</p>
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
          {contents}
        </div>
      </Fragment>
    )
  }
}

export const EventQ = withTranslation()(EventPlain);

export const Event = (props) => {
  const location = useLocation();

  return <EventQ {...props} event={location.state.event} seatsCount={location.state.seatsCount} seatsAvailableCount={location.state.seatsAvailableCount} />;
}
