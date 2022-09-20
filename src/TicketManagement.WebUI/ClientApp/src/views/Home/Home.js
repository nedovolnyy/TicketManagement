import React, { Component, Fragment } from 'react'
import { useTranslation, withTranslation } from 'react-i18next'
import EventImg from '../../components/EventImg'
import { ROLES } from '../../App'
import useAuth from '../../hooks/useAuth'
import { EventManagementApi } from '../../api/EventsManagementAPI'
import { configHTTPS } from '../../configurations/httpsConf'

class HomePlain extends Component {
  static displayName = HomePlain.name;

  constructor(props) {
    super(props);
    this.state = { events: [], loading: true };
  }

  componentDidMount() {
    this.getEvents();
  }

  async getEvents() {
    const EventClient = new EventManagementApi(configHTTPS);
    await EventClient.apiEventManagementEventsGet()
      .then(result => this.setState({ events: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderIndexPage(events) {
    return (
      <div className='container'>
        <div className='row'>
          {events.map(event =>
            <EventImg event={event} key={'EventImg' + event.id} allowedRoles={[ROLES.Administrator]} />
          )}
        </div>
      </div>
    );
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : HomePlain.renderIndexPage(this.state.events);

    return (
      <Fragment>
        <EventCount allowedRoles={[ROLES.Administrator, ROLES.EventManager]} count={this.state.events.length} />
        {contents}
      </Fragment>
    );
  }
}

export const Home = withTranslation()(HomePlain);

export const EventCount = ({ allowedRoles, count }) => {
  const { auth } = useAuth();
  const { t } = useTranslation();

  return (
    auth?.roles?.find(role => allowedRoles?.includes(role))
      ? (<div className="text-sm-center">
        <h5 className="display-4">{t('Total Events:')} {count}</h5>
      </div>)
      : false
  );
}
