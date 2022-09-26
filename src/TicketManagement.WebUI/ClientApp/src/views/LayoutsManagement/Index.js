import React, { useEffect, useState } from 'react'
import { withTranslation } from 'react-i18next'
import { LayoutManagementApi, VenueManagementApi } from '../../api/EventsManagementApi'
import { EventsManagementApiHTTPSconfig } from '../../configurations/httpsConf'

function LayoutsManagementPlain(props) {
  const { t } = props;
  const [layouts, setLayouts] = useState([]);
  const [venues, setVenues] = useState([]);
  const LayoutClient = new LayoutManagementApi(EventsManagementApiHTTPSconfig);
  const VenueClient = new VenueManagementApi(EventsManagementApiHTTPSconfig);

  useEffect(() => {
    (() => {
      LayoutClient.apiLayoutManagementLayoutsGet().then(result => setLayouts(result));
    })();
  },[]);

  useEffect(() => {
    (() => {
      VenueClient.apiVenueManagementVenuesGet().then(result => setVenues(result));
    })();
  },[]);

  function venueNameFromLayout(venueId) {
    for (var i = 0; i < venues.length; i++) {
      if (venues[i].id === venueId)
        return venues[i].name;
    }
  };

  return (
    <table className="table">
      <tbody>
        <tr><th>{t('Name')}</th><th>{t('Description')}</th><th>{t('Venue')}</th><th></th><th></th></tr>
        {layouts.map(layout => (
          <tr key={'tr_' + layout.id}>
            <td key={'td_name_' + layout.id}>{layout.name}</td>
            <td key={'tr_description_' + layout.id}>{layout.description}</td>
            <td key={'td_venue_' + layout.id}>{venueNameFromLayout(layout.venueId)}</td>
            <td>
              <form key={'Btns_' + layout.id} /*asp-action="Delete" asp-route-id="@layout.Id"*/ method="post">
                <button key={'editBtn_' + layout.id} className="btn btn-sm btn-primary" /*asp-action="Edit" asp-route-id="@layout.Id"*/>{t('Edit')}</button>
                <button key={'delBtn_' + layout.id} type="submit" className="btn btn-sm btn-danger">{t('Delete')}</button>
              </form>
            </td>
          </tr>
        ))}
        <tr>
          <td>
            <button className="btn btn-sm btn-block btn-success" /*asp-action="Create"*/>{t('Add layout')}</button>
          </td>
        </tr>
      </tbody>
    </table>
  );
}

export const LayoutsManagement = withTranslation()(LayoutsManagementPlain);
