import React, { Component } from "react"
import { Label, Input, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class UsersManagementCreatePlain extends Component {
  static displayName = UsersManagementCreatePlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <form /*UsersManagement/Create*/>
          <div className="form-group">
            <Label asp-for="Email" className="control-label" />
            <Input type="text" asp-for="Email" className="form-control" />
          </div>
          <div className="form-group">
            <Label asp-for="Password" className="control-label" />
            <Input type="password" asp-for="Password" className="form-control" />
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
            <Input type="submit" value={t('Add')} className="btn btn-outline-secondary" />
          </div>
        </form>
      </>
    );
  }
}

export const UsersManagementCreate = withTranslation()(UsersManagementCreatePlain);
