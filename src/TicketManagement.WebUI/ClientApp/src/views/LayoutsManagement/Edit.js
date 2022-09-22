import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class LayoutsManagementEditPlain extends Component {
  static displayName = LayoutsManagementEditPlain.name;

  /*
    var selectListVenueId = (await VenueManagementApiClient.GetAllVenuesAsync())
        ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        .ToList();
*/
  render() {
    const { t } = this.props;
    return (
      <>
        <Form asp-action="Edit" asp-controller="LayoutsManagement">
          <div asp-validation-summary="All" className="text-danger"></div>
          <div className="form-group">
            <label asp-for="Name" className="control-label"></label>
            <input type="text" asp-for="Name" className="form-control" />
          </div>
          <div className="form-group">
            <label asp-for="Description" className="control-label"></label>
            <input type="text" asp-for="Description" className="form-control" />
          </div>
          <div className="form-floating">
            <select asp-for="VenueId" asp-items="selectListVenueId" className="form-select"></select>
            <label className="form-label">{t('Venue')}</label>
          </div>
          <div className="form-group">
            <input type="submit" value="Save" className="btn btn-outline-secondary" />
          </div>
        </Form>
      </>
    );
  }
}

export const LayoutsManagementEdit = withTranslation()(LayoutsManagementEditPlain);
