import React from 'react';
import { useTranslation } from 'react-i18next';

export default function NoBalance() {
  const { t } = useTranslation();

  return (
    <>
      <table>
        <tbody>
          <tr>
            <td><h1>{t('Not enough funds on the balance')}</h1></td>
            <td><h2>{t('Your balance:')} @Model.Balance</h2></td>
            <td><h2>{t('To top up your balance, go to your')} <a href='cart'>{t('shopping cart')}</a>.</h2></td>
          </tr>
        </tbody>
      </table>
    </>
  );
}
