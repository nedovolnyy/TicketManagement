import React, { Fragment, useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { EventManagementApi } from '../api/EventsManagementApi'
import { ROLES } from '../App'
import useAuth from '../hooks/useAuth'
import { EventsManagementApiHTTPSconfig } from '../configurations/httpsConf'
import { DataNavigation } from 'react-data-navigation'
import { withRouter } from '../helpers/withRouter'
import { useNavigate } from 'react-router-dom'

function EventManagementButtonPlain({ eventId }) {
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
    event.preventDefault();

    DataNavigation.setData('eventIdForEdit', eventId);
    navigate('/EventsManagement/Edit', { state: { eventIdForEdit: eventId } });
  }

  const handleDeleteSubmit = (event) => {
    event.preventDefault();

    const conf = window.confirm(t('Are you sure you want to delete this event?'));
    if (conf) {
      const EventClient = new EventManagementApi(EventsManagementApiHTTPSconfig);
      EventClient.apiEventManagementEventEventIdDelete(eventId,
        {
          headers: { 'Content-Type': 'application/json', 'Authorization': 'Bearer '.concat(auth?.accessToken) },
          withCredentials: true
        }).then(response => {
          if (response.status === 200 || response.status === 204) {
            navigate('/', { replace: true });
          } else {
            console.log(response);
          }
        });
    }
  }

  return (
    auth?.roles?.find(role => allowedRoles?.includes(role))
      ? (
        <section>
          <form>
            <button type="button" className="btn btn-sm btn-primary" onClick={(event) => handleEditSubmit(event)} >{t('Edit')}</button>
            {canRemoveState &&
              <button type="button" className="btn btn-sm btn-danger" onClick={(event) => handleDeleteSubmit(event)} > {t('Delete')}</button>
            }
          </form>
        </section>)
      : (<Fragment></Fragment>)
  );
}

export const EventManagementButton = withRouter(EventManagementButtonPlain);
