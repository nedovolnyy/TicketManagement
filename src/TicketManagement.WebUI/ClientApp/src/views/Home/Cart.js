import React, { Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import useAuth from '../../hooks/useAuth'

function CartPlain(props) {
  const { t } = props;
  const { auth } = useAuth();

  return (
    <Fragment>
      <table>
        <tbody>
          <tr>
            <td><h1>{t('To up balance:')}</h1></td>
            <td>
              <div className='form-group'>
                <label asp-for='Balance' className='control-label' />
                <input asp-for='Balance' name='money' className='form-control' />
              </div>
              <div className='form-group'>
                <button type='submit' className='btn btn-outline-success'>{t('Add money')}</button>
              </div>
            </td>
          </tr>
        </tbody>
      </table>

      <h2>{t('Total purchase:')} {auth?.userResponse?.cartCount}</h2>
      <h1>{t('Purchase history:')}</h1>

      <h3>{auth?.userResponse?.payHistory}</h3>
    </Fragment>
  );
}

export const Cart = withTranslation()(CartPlain);
