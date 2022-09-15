import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"
import './SelectVenues.css'

class EventsManagementSelectVenuesPlain extends Component {
  static displayName = EventsManagementSelectVenuesPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <div className="form-group">
          <div className="wrap">
            <div className="left">
              <table className="table">
                <tbody>
                  <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Address')}</th><th>{t('Phone')}</th></tr>
                  {/* map */}
                  <tr>
                    <td>
                      @venue.Name;
                      <br />
                    </td>
                    <td>
                      @venue.Description;
                      <br />
                    </td>
                    <td>
                      @venue.Address;
                      <br />
                    </td>
                    <td>
                      @venue.Phone;
                      <br />
                    </td>
                  </tr>
                </tbody>
              </table>
            </div><div className="right">
              <table className="table">
                <tbody>
                  <tr><th></th></tr>
                  <tr>
                    <td>
                      <form asp-action="SelectLayouts" asp-controller="EventsManagement" method="post">
                        <input type="hidden" />
                        <div className="table-active">
                          {/* map */}
                          <input type="checkbox" name="venuesId" value="@venue.Id" />
                          <hr />
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
      </>
    );
  }
}

export const EventsManagementSelectVenues = withTranslation()(EventsManagementSelectVenuesPlain);
