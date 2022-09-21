import React, { Fragment, useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { EventManagementApi } from '../api/EventsManagementAPI'
import { ROLES } from '../App'
import useAuth from '../hooks/useAuth'
import { EventsManagementApiHTTPSconfig } from '../configurations/httpsConf'
import { DataNavigation } from 'react-data-navigation'
import { useNavigate } from 'react-router-dom'

export function EventManagementButton({ eventId }) {
  const { auth } = useAuth();
  const { t } = useTranslation();
  const navigate = useNavigate();
  const allowedRoles = [ROLES.Administrator, ROLES.EventManager];
  const [canRemoveState, setCanRemoveState] = useState(false);
  const client = new EventManagementApi(EventsManagementApiHTTPSconfig);

  useEffect(() => {
    (() => {
      client.apiEventManagementIsAllAvailableSeatsEventIdGet(eventId).then(result => setCanRemoveState(result));
    })();
  });

  const handleEditSubmit = (event) => {
    DataNavigation.setData('eventIdForEdit', eventId);
    navigate('/EventsManagement/Edit', { state: { eventIdForEdit: eventId } });
  }

  const handleDeleteSubmit = (event) => {
    const conf = window.confirm(t('Are you sure you want to delete this event?'));
    let number = parseFloat(event.target.parentNode.getAttribute("data-key"));
    if (conf) {
      const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
      EventClient.apiEventManagementEventEventIdDelete(eventId,
        {
          headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(auth?.accessToken) },
          withCredentials: true
        }).then(response => {
          if (response.status === 200 || response.status === 204) {
            console.log(response);
          } else {
            console.log(response);
          }
        });

      navigate('/', { replace: true });
    }
  }

  return (
    auth?.roles?.find(role => allowedRoles?.includes(role))
      ? (
        <section>
          <form>
            <button type="button" className="btn btn-sm btn-primary" onClick={handleEditSubmit} >{t('Edit')}</button>
            {canRemoveState &&
              <button type="button" className="btn btn-sm btn-danger" onClick={handleDeleteSubmit} > {t('Delete')}</button>
            }
          </form>
        </section>)
      : (<Fragment></Fragment>)
  );
}
