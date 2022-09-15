import React, { Component } from "react"
import { withTranslation } from "react-i18next"

class VenuesManagementPlain extends Component {
  static displayName = VenuesManagementPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <table className="table">
          <tbody>
            <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Address')}</th><th>{t('Phone')}</th><th></th><th></th></tr>
            <tr>
              <td>@venue.Name</td>
              <td>@venue.Description</td>
              <td>@venue.Address</td>
              <td>@venue.Phone</td>
              <td>
                <form asp-action="Delete" asp-route-id="@venue.Id" method="post">
                  <a className="btn btn-sm btn-primary" asp-action="Edit" asp-route-id="@venue.Id">{t('Edit')}</a>
                  <button type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
                </form>
              </td>
            </tr>
            <tr>
              <td>
                <a className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add venue')}</a>
              </td>
            </tr>
          </tbody>
        </table>
      </>
    );
  }
}

export const VenuesManagement = withTranslation()(VenuesManagementPlain);
