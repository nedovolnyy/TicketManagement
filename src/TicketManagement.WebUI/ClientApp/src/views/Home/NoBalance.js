import React, { Fragment } from 'react'
import { withTranslation } from 'react-i18next'
import useAuth from '../../hooks/useAuth'

function NoBalancePlain(props) {
  const { t } = props;
  const { auth } = useAuth();

  return (
    <Fragment>
      <table>
        <tbody>
          <tr>
            <td><h1>{t('Not enough funds on the balance')}</h1></td>
            <td><h2>{t('Your balance:')} {auth?.userResponse?.balance}</h2></td>
            <td><h2>{t('To top up your balance, go to your')} <a href='cart'>{t('shopping cart')}</a>.</h2></td>
          </tr>
        </tbody>
      </table>
    </Fragment>
  );
}

export const NoBalance = withTranslation()(NoBalancePlain);
