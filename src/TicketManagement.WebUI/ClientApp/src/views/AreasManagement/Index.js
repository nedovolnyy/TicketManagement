import React, { useEffect, useState } from "react"
import { withTranslation } from "react-i18next"
import { AreaManagementApi, LayoutManagementApi } from '../../api/EventsManagementAPI'
import { configHTTPS } from '../../configurations/httpsConf'

function AreasManagementPlain(props) {
  const { t } = props;
  const [areas, setAreas] = useState([]);
  const [layouts, setLayouts] = useState([]);
  const AreaClient = new AreaManagementApi(configHTTPS);
  const LayoutClient = new LayoutManagementApi(configHTTPS);

  useEffect(() => {
    (() => {
      AreaClient.apiAreaManagementAreasGet().then(result => setAreas(result));
    })();
  });

  useEffect(() => {
    (() => {
      LayoutClient.apiLayoutManagementLayoutsGet().then(result => setLayouts(result));
    })();
  });

  function layoutNameFromArea(layoutId) {
    for (var i=0; i< layouts.length;i++){
      if (layouts[i].id === layoutId)
        return layouts[i].name;
    }
  };

  return (
    <table className="table">
      <tbody>
        <tr><th>{t('Layout')}</th><th>{t('Description')}</th><th>{t('X')}</th><th>{t('Y')}</th><th></th><th></th></tr>
        {areas.map(area => (
          <tr key={'tr_' + area.id}>
            <td key={'td_layout_' + area.id}>{layoutNameFromArea(area.layoutId)}</td>
            <td key={'tr_description_' + area.id}>{area.description}</td>
            <td key={'tr_coordX_' + area.id}>{area.coordX}</td>
            <td key={'tr_coordY_' + area.id}>{area.coordY}</td>
            <td>
              <form key={'Btns_' + area.id} /*asp-action="Delete" asp-route-id="@area.Id"*/ method="post">
                <button key={'editBtn_' + area.id} className="btn btn-sm btn-primary" /*asp-action="Edit" asp-route-id="@area.Id"*/>{t('Edit')}</button>
                <button key={'delBtn_' + area.id} type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
              </form>
            </td>
          </tr>
        ))}
        <tr>
          <td>
            <button className="btn btn-sm btn-block btn-success" asp-action="Create">{t('Add area')}</button>
          </td>
        </tr>
      </tbody>
    </table>
  );
}

export const AreasManagement = withTranslation()(AreasManagementPlain);
