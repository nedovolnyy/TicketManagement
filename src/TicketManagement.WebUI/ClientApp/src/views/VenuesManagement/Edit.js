import React, { Component } from "react"
import { Label, Form, Input } from "reactstrap"
import { withTranslation } from "react-i18next"

class VenuesManagementEditPlain extends Component {
  static displayName = VenuesManagementEditPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <Form asp-action="Edit" asp-controller="VenuesManagement">
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
            <Input type="submit" value={t('Save')} className="btn btn-outline-secondary" />
          </div>
        </Form>
      </>
    );
  }
}

export const VenuesManagementEdit = withTranslation()(VenuesManagementEditPlain);
