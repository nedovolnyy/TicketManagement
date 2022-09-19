import React, { Component } from "react"
import { withTranslation } from "react-i18next"
import { useLocation } from 'react-router-dom'
import './Index.css'
import { EventAreaManagementApi } from '../../api/EventsManagementAPI'
import { EventAreasMap } from '../../components/EventAreasMap'
import { configHTTPS } from '../../configurations/httpsConf'

class EventPlain extends Component {
  static displayName = EventPlain.name;

  constructor(props) {
    super(props);
    this.state = { eventAreas: [], loading: true };
  }

  componentDidMount() {
    this.getEventAreas();
  }

  async getEventAreas() {
    const EventAreaClient = new EventAreaManagementApi(configHTTPS);
    await EventAreaClient.apiEventAreaManagementEventAreasByEventIdEventIdGet(this.props.event.id)
      .then(result => this.setState({ eventAreas: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  static renderIndexPage(eventAreas, event) {
    return (
      <EventAreasMap eventAreas={eventAreas} eventTime={event.eventTime} />
    );
  }

  render() {
    const { t } = this.props;
    const event = this.props.event;
    let contents = !this.state.loading ?
      EventPlain.renderIndexPage(this.state.eventAreas, event) : false
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
          <table className="table">
            <tbody>
              {contents}
            </tbody>
          </table>
        </div>
      </>
    )
  }
}

export const EventQ = withTranslation()(EventPlain);

export const Event = (props) => {
  const location = useLocation();

  return <EventQ {...props} event={location.state.event} seatsCount={location.state.seatsCount} seatsAvailableCount={location.state.seatsAvailableCount} />;
}
