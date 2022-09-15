import React, { Component } from "react"
import { Input, Label, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class UsersManagementEditPlain extends Component {
  static displayName = UsersManagementEditPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <Form asp-action="Edit" asp-controller="UsersManagement">
          <div asp-validation-summary="All" className="text-danger"></div>
          <div className="form-group">
            <Label asp-for="UserName" className="control-label" />
            <Input type="text" asp-for="UserName" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="Email" className="control-label" />
            <Input type="text" asp-for="Email" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="FirstName" className="control-label" />
            <Input type="text" asp-for="FirstName" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="SurName" className="control-label" />
            <Input type="text" asp-for="SurName" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="PhoneNumber" className="control-label" />
            <Input type="text" asp-for="PhoneNumber" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="PhoneNumber" className="control-label" />
            <Input type="text" asp-for="PhoneNumber" className="form-control" />
          </div>
          <div className="form-group">
            <Input type="submit" value={t('Save')} className="btn btn-outline-secondary" />
          </div>
        </Form>
      </>
    );
  }
}

export const UsersManagementEdit = withTranslation()(UsersManagementEditPlain);
