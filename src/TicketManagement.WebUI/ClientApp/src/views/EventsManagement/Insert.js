import React, { Component, Fragment } from 'react'
import { DataNavigation } from 'react-data-navigation'
import { withTranslation } from 'react-i18next'
import { EventManagementApi, LayoutManagementApi } from '../../api/EventsManagementApi'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { Auth } from '../../helpers/Auth'
import momentTZ from 'moment-timezone'
import moment from 'moment-timezone'
import { withRouter } from '../../helpers/withRouter'

class EventsManagementInsertPlain extends Component {
  static displayName = EventsManagementInsertPlain.name;

  constructor(props) {
    super(props);
    let date = moment(new Date()).format('yyyy-MM-DDThh:mm');
    this.state = {
      layouts: [], selectedLayouts: [], eventLayoutId: 0, eventName: '',
      eventDescription: '', eventEventLogoImage: '', eventEventPrice: 0.0, timeZone: '',
      eventEventTime: date, eventEventEndTime: date, render: false, firstRender: true
    };
  }

  componentDidMount() {
    this.getLayouts(DataNavigation.getData('selectedLayouts'));
    setTimeout(function () {
      this.setState({ render: true })
    }.bind(this), 1000);
  }

  getLayouts(startSelectedLayouts) {
    this.setState({ selectedLayouts: startSelectedLayouts }, () => {
      var selectedLayouts = this.state.selectedLayouts;
      this.setState({ eventLayoutId: +selectedLayouts[0] });
      const LayoutClient = new LayoutManagementApi(EventsManagementApiHTTPSconfig);
      selectedLayouts.map(layout => (
        LayoutClient.apiLayoutManagementLayoutLayoutIdGet(+layout)
          .then(result => this.setState({ layouts: [...this.state.layouts, result] }))
          .catch((error) => {
            console.log(error);
            alert(error);
          })
      ));
    });
  }

  handleSubmit(event) {
    event.preventDefault();

    this.setState({ firstRender: false });
/*
    let tempSelectedLayouts = [...this.state.selectedLayouts];
    let removingLayout = this.state.eventLayoutId;


    console.log(tempSelectedLayouts + '  :  ' + removingLayout);

    tempSelectedLayouts.splice(tempSelectedLayouts.indexOf(String(removingLayout))-1, 1)
    this.setState({ eventLayoutId: +tempSelectedLayouts[0] });

    React.unmountComponentAtNode(document.getElementById('optLayoutCreate'.concat(removingLayout)));

    console.log('[id="optLayout' + removingLayout + '"]');


    this.getLayouts(tempSelectedLayouts);

    console.log(this.state.selectedLayouts);

    if (this.state.selectedLayouts.length === 1)
      this.props.router.navigate('/');
 */
    const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
    EventClient.apiEventManagementEventPost(this.state.eventEventPrice, JSON.stringify({
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
  }

  render() {
    const { t } = this.props;
    const timeZonesList = momentTZ.tz.names();
    return (
      (!this.state.render) ? (<p><em>{t('Loading...')}</em></p>) : (
        <Fragment>
          <div className="row">
            <div className="col-md-6">
              <form id="profile-form">
                <div className="form-floating">
                  <input type="text" id="eventName" className="form-control" required
                    onChange={(event) => this.setState({ eventName: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventName">{t('Name')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="text" id="eventDescription" className="form-control" required
                    onChange={(event) => this.setState({ eventDescription: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventDescription">{t('Description')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="datetime-local" id="eventEventTime" className="form-control" value={this.state.eventEventTime} required
                    onChange={(event) => this.setState({ eventEventTime: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventTime">{t('EventTime')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <select name="timeZone" className="form-select" value={this.state.timeZone} required
                    onChange={(event) => this.setState({ timeZone: event.target.value })}>
                    {timeZonesList.map(timeZone => {
                      return <option key={timeZone}>{timeZone}</option>
                    })}
                  </select>
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted">{t('Time Zone for Event')}</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="datetime-local" id="eventEventEndTime" className="form-control" value={this.state.eventEventEndTime} required
                    onChange={(event) => this.setState({ eventEventEndTime: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventEndTime">{t('EventEndTime')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="text" id="eventEventLogoImage" className="form-control" required
                    onChange={(event) => this.setState({ eventEventLogoImage: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventEventLogoImage">{t('EventLogoImage')}:</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <select name="layoutId" id="selectListLayoutsId" className="form-select" value={this.state.eventLayoutId} required
                    onChange={(event) => this.setState({ eventLayoutId: event.target.value })}>
                    {this.state.firstRender &&
                      this.state.layouts.map(layout => {
                        return <option id={"optLayoutCreate".concat(layout.id)} key={"optLayoutCreate".concat(layout.id)} value={layout.id}>{layout.name}</option>
                      })}
                  </select>
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="selectListLayoutsId">{t('Layout')}</label>
                </div>
                <br></br>
                <div className="form-floating">
                  <input type="currency" id="eventPrice" className="form-control" required
                    onChange={(event) => this.setState({ eventEventPrice: event.target.value })} />
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="eventPrice">{t('Price')}</label>
                </div>
                <br></br>
                <button type="button" className="w-100 btn btn-lg btn-primary" onClick={(event) => this.handleSubmit(event)}>{t('Create Event')}</button>
              </form>
            </div>
          </div >
        </Fragment >
      )
    );
  }
}

export const EventsManagementInsert = Auth(withRouter(withTranslation()(EventsManagementInsertPlain)));
