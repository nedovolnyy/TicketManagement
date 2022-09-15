import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class LayoutsManagementPlain extends Component {
  static displayName = LayoutsManagementPlain.name;

  /*
      var selectListVenueId = (await VenueManagementApiClient.GetAllVenuesAsync())
          ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
  */
  render() {
    const { t } = this.props;
    return (
      <>
        <table className="table">
          <tbody>
            <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Venue')}</th><th></th><th></th></tr>
            { /* map */}
            <tr>
              <td>@layout.Name</td>
              <td>@layout.Description</td>
              <td>@selectListVenueId.Where(x=>x.Value == layout.VenueId.ToString()).First().Text</td>
              <td>
                <Form asp-action="Delete" asp-route-id="@layout.Id" method="post">
                  <Button className="btn btn-sm btn-primary" asp-action="Edit" asp-route-id="@layout.Id">{t('Edit')}</Button>>
                  <Button type="submit" className="btn btn-sm btn-danger">{t('Delete')}</Button>
                </Form>
              </td>
            </tr>
            <tr>
              <td>
                <a className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add layout')}</a>
              </td>
            </tr>
          </tbody>
        </table>
      </>
    );
  }
}

export const LayoutsManagement = withTranslation()(LayoutsManagementPlain);
