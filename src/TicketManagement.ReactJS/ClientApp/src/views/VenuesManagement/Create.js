import React, { Component } from "react"
import { Input, Label, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class VenuesManagementCreatePlain extends Component {
  static displayName = VenuesManagementCreatePlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <Form asp-action="Create" asp-controller="VenuesManagement">
          <div asp-validation-summary="All" className="text-danger"></div>
          <div className="form-group">
            <Label asp-for="Name" className="control-label" />
            <Input type="text" asp-for="Name" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="Description" className="control-label" />
            <Input type="text" asp-for="Description" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="Address" className="control-label" />
            <Input type="text" asp-for="Address" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="Phone" className="control-label" />
            <Input type="text" asp-for="Phone" className="form-control" />
          </div>
          <div className="form-group">
            <Input type="submit" value={t('Add')} className="btn btn-outline-secondary" />
          </div>
        </Form>
      </>
    );
  }
}

export const VenuesManagementCreate = withTranslation()(VenuesManagementCreatePlain);
