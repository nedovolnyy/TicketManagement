import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import momentTZ from 'moment-timezone'
import moment from 'moment-timezone'
import { Auth } from '../../helpers/Auth'
import { withRouter } from '../../helpers/withRouter'
import { EventManagementApi, LayoutManagementApi } from '../../api/EventsManagementApi'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { DataNavigation } from 'react-data-navigation'

class EventsManagementEditPlain extends Component {
  static displayName = EventsManagementEditPlain.name;

  constructor(props) {
    super(props);
    let date = moment(new Date()).format('yyyy-MM-DDThh:mm');
    this.state = {
      layouts: [], eventId: 0, event: [], eventLayoutId: 1, eventName: '',
      eventDescription: '', eventEventLogoImage: '', eventEventPrice: 0.0, timeZone: '',
      eventEventTime: date, eventEventEndTime: date, render: false, firstRender: true, canRemoveState: false
    };
  }

  componentDidMount() {
    this.setState({ eventId: DataNavigation.getData('eventIdForEdit') }, () => this.getEvent(this.state.eventId));
    this.getLayouts();
    setTimeout(function () {
      this.setState({ render: true })
    }.bind(this), 1000);
  }

  getEvent(eventId) {
    const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
    EventClient.apiEventManagementEventEventIdGet(eventId)
      .then(result => this.setState({ event: result }, () => {
        EventClient.apiEventManagementIsAllAvailableSeatsEventIdGet(eventId).then(result => this.setState({ canRemoveState: result }));
      }))
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  getLayouts() {
    const LayoutClient = new LayoutManagementApi(EventsManagementApiHTTPSconfig);
    LayoutClient.apiLayoutManagementLayoutsGet()
      .then(result => this.setState({ layouts: result }))
      .catch((error) => {
        console.log(error);
        alert(error);
      })
  }

  handleSubmit(event) {
    event.preventDefault();

    const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
    EventClient.apiEventManagementEventPut(this.state.eventEventPrice, JSON.stringify({
      id: this.state.eventId,
      name: this.state.eventName,
      eventTime: this.state.eventEventTime,
      description: this.state.eventDescription,
      layoutId: this.state.eventLayoutId,
      eventEndTime: this.state.eventEventEndTime,
      eventLogoImage: this.state.eventEventLogoImage
    }),
      {
        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(this.props.auth?.accessToken) },
        withCredentials: true
      }).then(response => {
        if (response.status === 200 || response.status === 204) {
          console.log(response);
        } else {
          console.log(response);
        }
      });

    this.props.router.navigate(this.props.routes.location.state?.from?.pathname || "/", { replace: true });
  }

  render() {
    const { t } = this.props;
    const timeZonesList = momentTZ.tz.names();
    const eventLayout = this.state.layouts[this.state.event.layoutId];
    let eventTime = moment(this.state.event.eventTime).format('yyyy-MM-DDThh:mm');
    let eventEndTime = moment(new Date()).format('yyyy-MM-DDThh:mm');
    return (
      (!this.state.render) ? (<p><em>{t('Loading...')}</em></p>) : (
        <Fragment>
          <div className="row">
            <div className="col-md-6">
              <form id="profile-form">
                <div className="form-floating">
                  <input type="text" id="eventName" defaultValue={this.state.event.name} value={this.state.eventName} className="form-control" required
                    onChange={(event) => (event.preventDefault(), this.setState({ eventName: event.target.value }))} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventName">{t('Name')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="text" id="eventDescription" defaultValue={this.state.event.description} value={this.state.eventDescription} className="form-control" required
                    onChange={(event) => { event.preventDefault(); this.setState({ eventDescription: event.target.value }) }} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventDescription">{t('Description')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="datetime-local" id="eventEventTime" defaultValue={eventTime} className="form-control" value={this.state.eventEventTime} required
                    onChange={(event) => { event.preventDefault(); this.setState({ eventEventTime: event.target.value }) }} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventTime">{t('EventTime')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <select name="timeZone" className="form-select" value={this.state.timeZone} required
                    onChange={(event) => { event.preventDefault(); this.setState({ timeZone: event.target.value }) }}>
                    {timeZonesList.map(timeZone => {
                      return <option key={timeZone}>{timeZone}</option>
                    })}
                  </select>
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted">{t('Time Zone for Event')}</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="datetime-local" id="eventEventEndTime" defaultValue={eventEndTime} className="form-control" value={this.state.eventEventEndTime} required
                    onChange={(event) => { event.preventDefault(); this.setState({ eventEventEndTime: event.target.value }) }} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventEndTime">{t('EventEndTime')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="text" id="eventEventLogoImage" defaultValue={this.state.event.eventLogoImage} value={this.state.eventEventLogoImage} className="form-control" required
                    onChange={(event) => { event.preventDefault(); this.setState({ eventEventLogoImage: event.target.value }) }} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventLogoImage">{t('EventLogoImage')}:</label>
                </div>
                <br></br>
                <div className="form-floating">{(this.state.canRemoveState) ? (
                  <select id="selectListLayoutsId" defaultValue={this.state.event.layoutId} className="form-select" value={this.state.eventLayoutId} required
                    onChange={(event) => { event.preventDefault(); this.setState({ eventLayoutId: event.target.value }) }}>
                    {this.state.firstRender &&
                      this.state.layouts.map(layout => {
                        return <option id={"optLayout".concat(layout.id)} key={"optLayout".concat(layout.id)} value={layout.id}>{layout.name}</option>
                      })}
                  </select>) : (<input className="form-control" id="selectListLayoutsId" value={eventLayout.name} disabled></input>)}
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="selectListLayoutsId">{t('Layout')}</label>
                </div>
                <br></br>
                <div className="form-floating">{(this.state.canRemoveState) ? (<input type="currency" id="eventPrice" required className="currencyInput form-control"
                  onChange={(event) => this.setState({ eventEventPrice: event.target.value })}></input>) :
                  (<input type="currency" id="eventPrice" required className="currencyInput form-control" disabled></input>)}
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventPricePrice" ></label>
                </div>
                <br></br>
                <button type="button" className="w-100 btn btn-lg btn-primary" onClick={(event) => this.handleSubmit(event)}>{t('Edit Event')}</button>
              </form>
            </div>
          </div >
        </Fragment >
      ));
  }
}

export const EventsManagementEdit = Auth(withRouter(withTranslation()(EventsManagementEditPlain)));
