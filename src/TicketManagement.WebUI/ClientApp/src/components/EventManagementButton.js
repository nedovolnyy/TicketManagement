import React, { useEffect, useState } from 'react'
import { useTranslation } from 'react-i18next'
import { EventManagementApi } from '../api/EventsManagementAPI'
import { ROLES } from '../App'
import useAuth from '../hooks/useAuth'
import { configHTTPS } from '../configurations/httpsConf'

export function EventManagementButton({ eventId }) {
  const { auth } = useAuth();
  const { t } = useTranslation();
  const allowedRoles = [ROLES.Administrator, ROLES.EventManager];
  const [canRemoveState, setCanRemoveState] = useState(false);
  const client = new EventManagementApi(configHTTPS);

  useEffect(() => {
    (() => {
      client.apiEventManagementIsAllAvailableSeatsEventIdGet(eventId).then(result => setCanRemoveState(result));
    })();
  });

  return (
    auth?.roles?.find(role => allowedRoles?.includes(role))
      ? (
        <section>
          <form /*"EventsManagement/Delete:{eventId}"*/ method="post">
            <label className="btn btn-sm btn-primary" /*"EventsManagement/Edit:{eventId}"*/ >{t('Edit')}</label>
            {canRemoveState &&
              <button type="submit" className="btn btn-sm btn-danger" onClick={() => window.confirm(t('Are you sure you want to delete this event?'))} > {t('Delete')}</button>
            }
          </form>
        </section>)
      : (<></>)
  );
}
