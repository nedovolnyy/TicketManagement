import React, { Component, Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import { UsersManagementApi } from '../../api/UsersManagementApi'
import { UsersManagementApiHTTPSconfig } from '../../configurations/httpsConf'

class UsersManagementPlain extends Component {
  static displayName = UsersManagementPlain.name;

  constructor(props) {
    super(props);
    this.state = { users: [], loading: true };
  }

  componentDidMount() {
    this.getUsers();
  }

  async getUsers() {
    const UserClient = new UsersManagementApi(UsersManagementApiHTTPSconfig);
    await UserClient.apiUsersManagementUsersGet()
      .then(result => this.setState({ users: result, loading: false }))
      .catch((error) => {
        console.log(error);
        alert(error);
      });
  }

  static renderContent(users, props) {
    const { t } = props;
    return (
      <table className="table">
        <tbody>
          <tr><th>{t('User Name')}</th><th>{t('First Name')}</th><th>{t('Sur Name')}</th><th>{t('Email')}</th><th>{t('PhoneNumber')}</th><th>{t('Role')}</th><th></th></tr>
          {users.map(user => (
            <tr key={'tr_' + user.id}>
              <td key={'td_userName_' + user.id}>{user.userName}</td>
              <td key={'td_firstName_' + user.id}>{user.firstName}</td>
              <td key={'td_surName_' + user.id}>{user.surName}</td>
              <td key={'td_email_' + user.id}>{user.email}</td>
              <td key={'td_phoneNumber_' + user.id}>{user.phoneNumber}</td>
              <td>
                <button key={'changeRoleBtn_' + user.id} className="btn btn-sm btn-primary" /*asp-action="ChangeRole" asp-route-userid="@user.Id"*/>{t('Change role')}</button>
              </td>
              <td>
                <form key={'Btns_' + user.id} /*asp-action="Delete" asp-route-id="@user.Id"*/ method="post">
                  <button key={'editBtn_' + user.id} className="btn btn-sm btn-primary" /*asp-action="Edit" asp-route-id="@user.Id"*/>{t('Edit')}</button>
                  <button key={'delBtn_' + user.id} type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
                </form>
              </td>
            </tr>
          ))}
          <tr>
            <td>
              <button className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add user')}</button>
            </td>
          </tr>
        </tbody>
      </table>
    )
  }

  render() {
    const { t } = this.props;
    let contents = this.state.loading
      ? <p><em>{t('Loading...')}</em></p>
      : UsersManagementPlain.renderContent(this.state.users, this.props);

    return (
      <Fragment>
        {contents}
      </Fragment>
    );
  }
}

export const UsersManagement = withTranslation()(UsersManagementPlain);
