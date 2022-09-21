import React, { Component, Fragment } from 'react'
import './SelectVenues.css'
import { withTranslation } from 'react-i18next'
import { VenueManagementApi } from '../../api/EventsManagementAPI'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'

class EventsManagementSelectVenuesPlain extends Component {
  static displayName = EventsManagementSelectVenuesPlain.name;

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
    return (
      <Fragment>
        <div className="form-group">
          <div className="wrap">
            <div className="venues_left">
              <table className="table">
                <tbody>
                  <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Address')}</th><th>{t('Phone')}</th></tr>
                  {venues.map(venue => (
                    <tr key={"tr_".concat(venue.id)}>
                      <td key={"td_name_".concat(venue.id)} >
                        {venue.name}
                        <br key={"br1_".concat(venue.id)} />
                      </td>
                      <td key={"td_description_".concat(venue.id)}>
                        {venue.description}
                        <br key={"br2_".concat(venue.id)} />
                      </td>
                      <td key={"td_address_".concat(venue.id)}>
                        {venue.address}
                        <br key={"br3_".concat(venue.id)} />
                      </td>
                      <td key={"td_phone_".concat(venue.id)}>
                        {venue.phone}
                        <br key={"br4_".concat(venue.id)} />
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div><div className="venues_right">
              <table className="table">
                <tbody>
                  <tr><th></th></tr>
                  <tr>
                    <td>
                      <form /*asp-action="SelectLayouts" asp-controller="EventsManagement"*/ method="post">
                        <input type="hidden" />
                        <div className="table-active">
                          {venues.map(venue => (
                            <Fragment key={"fr_".concat(venue.id)} >
                              <input key={"checkbox_".concat(venue.id)} type="checkbox" name="venuesId" value="@venue.Id" />
                              <hr key={"hr".concat(venue.id)} />
                            </Fragment>
                          ))}
                        </div>
                        <button type="submit" className="btn btn-primary">{t('Select')}</button>
                      </form>
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>
        </div>
      </Fragment>
    )
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : EventsManagementSelectVenuesPlain.renderContent(this.state.venues, this.props);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const EventsManagementSelectVenues = withTranslation()(EventsManagementSelectVenuesPlain);
