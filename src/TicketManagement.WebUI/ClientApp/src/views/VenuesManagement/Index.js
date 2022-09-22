import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import { VenueManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'

class VenuesManagementPlain extends Component {
  static displayName = VenuesManagementPlain.name;

  constructor(props) {
    super(props);
    this.state = { venues: [], loading: true };
  }

  componentDidMount() {
    this.getVenues();
  }

  async getVenues() {
    const VenueClient = new VenueManagementApi(EventsManagementApiHTTPSconfig);
    await VenueClient.apiVenueManagementVenuesGet()
      .then(result => this.setState({ venues: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderContent(venues, props) {
    const { t } = props;
    return (<table className="table">
      <tbody>
        <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Address')}</th><th>{t('Phone')}</th><th></th><th></th></tr>
        {venues.map(venue => (
            <tr key={'tr_' + venue.id}>
              <td key={'td_name_' + venue.id}>{venue.name}</td>
              <td key={'tr_description_' + venue.id}>{venue.description}</td>
              <td key={'tr_address_' + venue.id}>{venue.address}</td>
              <td key={'tr_phone_' + venue.id}>{venue.phone}</td>
              <td>
                <form key={'Btns_' + venue.id} /*asp-action="Delete" asp-route-id="@venue.Id"*/ method="post">
                  <button key={'editBtn_' + venue.id} className="btn btn-sm btn-primary" /*asp-action="Edit" asp-route-id="@venue.Id"*/>{t('Edit')}</button>
                  <button key={'delBtn_' + venue.id} type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
                </form>
              </td>
            </tr>
        ))}
        <tr>
          <td>
            <button className="btn btn-sm btn-block btn-success" /*asp-action="Create"*/>{t('Add venue')}</button>
          </td>
        </tr>
      </tbody>
    </table>
    );
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : VenuesManagementPlain.renderContent(this.state.venues, this.props);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const VenuesManagement = withTranslation()(VenuesManagementPlain);
