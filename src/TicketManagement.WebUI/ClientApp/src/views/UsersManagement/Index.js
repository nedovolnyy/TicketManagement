import React, { Component } from "react"
import { Button, Form } from "reactstrap"
import { withTranslation } from "react-i18next"

class UsersManagementPlain extends Component {
  static displayName = UsersManagementPlain.name;

  render() {
    const { t } = this.props;
    return (
      <>
        <table className="table">
          <tbody>
            <tr><th>{t('User Name')}</th><th>{t('First Name')}</th><th>{t('Sur Name')}</th><th>{t('Email')}</th><th>{t('PhoneNumber')}</th><th>{t('Role')}</th><th></th></tr>
            <tr>
              <td>@user.UserName</td>
              <td>@user.FirstName</td>
              <td>@user.SurName</td>
              <td>@user.Email</td>
              <td>@user.PhoneNumber</td>
              <td>
                <a className="btn btn-sm btn-primary" asp-action="ChangeRole" asp-route-userid="@user.Id">{t('Change role')}</a>
              </td>
              <td>
                <form asp-action="Delete" asp-route-id="@user.Id" method="post">
                  <a className="btn btn-sm btn-primary" asp-action="Edit" asp-route-id="@user.Id">{t('Edit')}</a>
                  <button type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
                </form>
              </td>
            </tr>
            <tr>
              <td>
                <a className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add user')}</a>
              </td>
            </tr>
          </tbody>
        </table>
      </>
    );
  }
}

export const UsersManagement = withTranslation()(UsersManagementPlain);
