import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class AreasManagementPlain extends Component {
  static displayName = AreasManagementPlain.name;

  /*
    var selectListLayoutsId = (await LayoutManagementApiClient.GetAllLayoutsAsync())
        ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name });
  */

  render() {
    const { t } = this.props;
    return (
      <>
        <table className="table">
          <tbody>
            <tr><th>{t('Layout')}</th><th>{t('Description')}</th><th>{t('X')}</th><th>{t('Y')}</th><th></th><th></th></tr>
            {/* map (var area in Model) */}
            <tr>
              <td>@selectListLayoutsId.Where(x=>x.Value == area.LayoutId.ToString()).First().Text</td>
              <td>@area.Description</td>
              <td>@area.CoordX</td>
              <td>@area.CoordY</td>
              <td>
                <form asp-action="Delete" asp-route-id="@area.Id" method="post">
                  <button className="btn btn-sm btn-primary" asp-action="Edit" asp-route-id="@area.Id">{t('Edit')}</button>
                  <button type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
                </form>
              </td>
            </tr>
            <tr>
              <td>
                <button className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add area')}</button>
              </td>
            </tr>
          </tbody>
        </table>
      </>
    );
  }
}

export const AreasManagement = withTranslation()(AreasManagementPlain);
