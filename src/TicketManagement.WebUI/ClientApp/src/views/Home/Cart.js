import React, { Component, Fragment } from 'react'
import { render } from 'react-dom';
import { withTranslation } from 'react-i18next'
import { UsersManagementApi } from '../../api/UsersManagementApi';
import { UsersManagementApiHTTPSconfig } from '../../configurations/httpsConf';
import { Auth } from '../../helpers/Auth'
import { withRouter } from '../../helpers/withRouter';

class CartPlain extends Component {
  static displayName = CartPlain.name;

  constructor(props) {
    super(props);
    this.state = { balance: 0.0 }
  }

  componentDidMount() {
    this.setState({ balance: this.props.auth?.userResponse?.balance });
  }

  changeBalance(event) {
    event.preventDefault();
    const UserClient = new UsersManagementApi(UsersManagementApiHTTPSconfig);
    // ToDo: update auth?.user
    UserClient.apiUsersManagementCartPut(this.state.balance, this.props.auth?.userResponse?.id,
      {
        headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(this.props.auth?.accessToken) },
        withCredentials: true
      })
      .catch((error) => {
        console.log(error);
        alert(error);
      });
    this.props.router.navigate('/', { replace: true });
  }

  render() {
    const { t } = this.props;
    return (
      <Fragment>
        <table>
          <tbody>
            <tr>
              <td><h1>{t('To up balance:')}</h1></td>
              <td>
                <div className='form-group'>
                  <input id='Balance' name='money' className='form-control' value={this.state.balance}
                    onChange={(event) => { event.preventDefault(); this.setState({ balance: event.target.value }) }} />
                  <label htmlFor='Balance' className='col-sm-2 col-form-label col-form-label-sm text-muted' />
                </div>
                <div className='form-group'>
                  <button type='button' className='btn btn-outline-success' onClick={(event) => this.changeBalance(event)}>{t('Add money')}</button>
                </div>
              </td>
            </tr>
          </tbody>
        </table>

        <h2>{t('Total purchase:')} {this.props.auth?.userResponse?.cartCount}</h2>
        <h1>{t('Purchase history:')}</h1>

        <h3>{this.props.auth?.userResponse?.payHistory}</h3>
      </Fragment>
    );
  }
}
export const Cart = Auth(withRouter(withTranslation()(CartPlain)));
