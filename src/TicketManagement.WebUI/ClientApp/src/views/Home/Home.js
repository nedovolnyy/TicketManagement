import React, { Component, Fragment } from 'react'
import './Home.css'
import { withTranslation } from 'react-i18next'
import EventImg from '../../components/EventImg'
import { ROLES } from '../../App'
import { EventManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'
import { Auth } from '../../helpers/Auth'
import { Container } from 'reactstrap'

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
    const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
    await EventClient.apiEventManagementEventsGet()
      .then(result => this.setState({ events: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderIndexPage(events) {
    return (
      <Container>
        <div className='row'>
          {events.map(event =>
            <EventImg event={event} key={'EventImg' + event.id} allowedRoles={[ROLES.Administrator]} />
          )}
        </div>
      </Container>
    );
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : HomePlain.renderIndexPage(this.state.events);

    return (
      <Fragment>
        {this.props.auth?.roles?.find(role => [ROLES.Administrator, ROLES.EventManager].includes(role)) &&
          <div className="text-sm-center">
            <h5 className="display-4">{t('Total Events:')} {this.state.events.length}</h5>
          </div>
        }
        {contents}
      </Fragment>
    );
  }
}

export const Home = Auth(withTranslation()(HomePlain));
