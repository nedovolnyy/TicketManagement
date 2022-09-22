import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class UsersManagementChangeRolePlain extends Component {
  static displayName = UsersManagementChangeRolePlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <h2>{t('Change role for')} @Model.UserEmail</h2>
        <form asp-action="ChangeRole" method="post">
          <input type="hidden" name="userId" value="@Model.UserId" />
          <div className="form-group">
            {/* map 
              <input type="checkbox" name="roles" value="@role.Name"
                   @(Model.UserRoles.Contains(role.Name) ? "checked=\"checked\"" : "") />@role.Name <br />*/ }
          </div>
          <button type="submit" className="btn btn-primary">{t('Change')}</button>
        </form>
      </>
    );
  }
}

export const UsersManagementChangeRole = withTranslation()(UsersManagementChangeRolePlain);
