import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { events: [], loading: true };
    }

    componentDidMount() {
        this.populateEventsData();
    }

    static renderEventsTable(events) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>name</th>
                        <th>eventTime. (C)</th>
                        <th>description. (F)</th>
                        <th>layoutId</th>
                        <th>eventEndTime</th>
                    </tr>
                </thead>
                <tbody>
                    {events.map(event =>
                        <tr key={event.id}>
                            <td>{event.name}</td>
                            <td>{event.eventTime}</td>
                            <td>{event.description}</td>
                            <td>{event.layoutId}</td>
                            <td>{event.eventEndTime}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderEventsTable(this.state.events);

        return (
            <div>
                <h1 id="tabelLabel" >Weather forecast</h1>
                <p>This component demonstrates fetching data from the server.</p>
                {contents}
            </div>
        );
    }

    async populateEventsData() {
        const response = await fetch('https://localhost:5003/api/EventManagement/events');
        const data = await response.json();
        this.setState({ events: data, loading: false });
    }
}
