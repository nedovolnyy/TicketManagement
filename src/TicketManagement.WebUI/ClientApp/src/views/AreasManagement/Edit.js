import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class AreasManagementEditPlain extends Component {
  static displayName = AreasManagementEditPlain.name;

  /*
    var selectListLayoutsId = (await LayoutManagementApiClient.GetAllLayoutsAsync())
        ?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name })
        .ToList();
  */

  render() {
    const { t } = this.props;
    return (
      <>
        <form asp-action="Edit" asp-controller="AreasManagement">
          <div asp-validation-summary="All" className="text-danger"></div>
          <div className="form-floating">
            <select asp-for="LayoutId" asp-items="selectListLayoutsId" className="form-select"></select>
            <label className="form-label">{t('Layout')}</label>
          </div>
          <div className="form-group">
            <label asp-for="Description" className="control-label"></label>
            <input type="text" asp-for="Description" className="form-control" />
          </div>
          <div className="form-group">
            <label asp-for="CoordX" className="control-label"></label>
            <input type="text" asp-for="CoordX" className="form-control" />
          </div>
          <div className="form-group">
            <label asp-for="CoordY" className="control-label"></label>
            <input type="text" asp-for="CoordY" className="form-control" />
          </div>
          <div className="form-group">
            <input type="submit" value="Save" className="btn btn-outline-secondary" />
          </div>
        </form>
      </>
    );
  }
}

export const AreasManagementEdit = withTranslation()(AreasManagementEditPlain);
