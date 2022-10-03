import React, { Component } from "react"
import { withTranslation } from "react-i18next"

class LayoutsManagementCreatePlain extends Component {
  static displayName = LayoutsManagementCreatePlain.name;

  /*
      var selectListVenueId = (await VenueManagementApiClient.GetAllVenuesAsync())
          ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
          .ToList();
    */
  render() {
    const { t } = this.props;
    return (
      <>
        <form asp-action="Create" asp-controller="LayoutsManagement">
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
            <label className="form-label">VenueId</label>
          </div>
          <div className="form-group">
            <input type="submit" value="Add" className="btn btn-outline-secondary" />
          </div>
        </form>
      </>
    );
  }
}

export const LayoutsManagementCreate = withTranslation()(LayoutsManagementCreatePlain);
