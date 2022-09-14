import React, { Component } from 'react';
import EventImg from '..//../components/EventImg';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { events: [], loading: true };
  }

  componentDidMount() {
    this.getEvents();
  }

  static renderIndexPage(events) {
    return (
      <div className='container'>
        <div className='row'>
          {events.map(event =>
            <EventImg event={event} key={'EventImg' + event.id} />
          )}
        </div>
      </div>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : Home.renderIndexPage(this.state.events);

    return (
      <div>
        <p>This component demonstrates fetching data from the server.</p>
        {contents}
      </div>
    );
  }

  async getEvents() {
    const url = 'https://localhost:5003/api/EventManagement/events';
    await fetch(url, {
      method: 'GET'
    })
      .then(response => response.json())
      .then(eventsFromServer => {
        this.setState({ events: eventsFromServer, loading: false });
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }
}
