import React from 'react';
import { useTranslation } from 'react-i18next';

export default function Cart() {
  const { t } = useTranslation();

  return (
    <>
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

      <h2>{t('Total purchase:')} @Model.CartCount</h2>
      <h1>{t('Purchase history:')}</h1>

      <h3>@Model.PayHistory</h3>
    </>
  );
}
