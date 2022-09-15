import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './views/Layout';
import './custom.css';
import RequireAuth from './helpers/RequireAuth';
import Register from './views/Identity/Register';
import Login from './views/Identity/Login';
import Logout from './views/Identity/Logout';
import Missing from './views/Identity/Missing';
import Unauthorized from './views/Identity/Unauthorized';
import Cart from "./views/Home/Cart";
import NoBalance from "./views/Home/NoBalance";
import { Home } from "./views/Home/Home";
import { ThirdPartyEvents } from './views/ThirdPartyEvents/Preview';
import { VenuesManagement } from './views/VenuesManagement/Index';
import { VenuesManagementCreate } from './views/VenuesManagement/Create';
import { VenuesManagementEdit } from './views/VenuesManagement/Edit';
import { AreasManagementCreate } from './views/AreasManagement/Create';
import { AreasManagementEdit } from './views/AreasManagement/Edit';
import { AreasManagement } from './views/AreasManagement/Index';
import { Event } from './views/Event/Index';
import { EventsManagementEdit } from './views/EventsManagement/Edit';
import { EventsManagementInsert } from './views/EventsManagement/Insert';
import { EventsManagementSelectLayouts } from './views/EventsManagement/SelectLayouts';
import { EventsManagementSelectVenues } from './views/EventsManagement/SelectVenues';
import { LayoutsManagement } from './views/LayoutsManagement/Index';
import { LayoutsManagementCreate } from './views/LayoutsManagement/Create';
import { LayoutsManagementEdit } from './views/LayoutsManagement/Edit';
import { UsersManagement } from './views/UsersManagement/Index';
import { UsersManagementCreate } from './views/UsersManagement/Create';
import { UsersManagementEdit } from './views/UsersManagement/Edit';
import { UsersManagementChangeRole } from './views/UsersManagement/ChangeRole';

export const ROLES = {
  'Administrator': 5150,
  'User': 2001,
  'EventManager': 1984,
}

function App() {
  return (
    <Layout>
      <Routes>
        {/* public routes */}
        <Route path="/" element={<Home />} />
        <Route path="Event" element={<Event />} />
        <Route path="Identity/Account/Register" element={<Register />} />
        <Route path="Identity/Account/Login" element={<Login />} />
        <Route path="Identity/Account/Logout" element={<Logout />} />
        <Route path="unauthorized" element={<Unauthorized />} />

        {/* we want to protect these routes */}
        <Route element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Administrator, ROLES.EventManager]} />}>
          <Route path="cart" element={<Cart />} />
          <Route path="nobalance" element={<NoBalance />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={[ROLES.Administrator]} />}>
          <Route path="AreasManagement" element={<AreasManagement />} />
          <Route path="AreasManagement/Create" element={<AreasManagementCreate />} />
          <Route path="AreasManagement/Edit" element={<AreasManagementEdit />} />
          <Route path="LayoutsManagement" element={<LayoutsManagement />} />
          <Route path="LayoutsManagement/Create" element={<LayoutsManagementCreate />} />
          <Route path="LayoutsManagement/Edit" element={<LayoutsManagementEdit />} />
          <Route path="UsersManagement" element={<UsersManagement />} />
          <Route path="UsersManagement/Create" element={<UsersManagementCreate />} />
          <Route path="UsersManagement/Edit" element={<UsersManagementEdit />} />
          <Route path="UsersManagement/ChangeRole" element={<UsersManagementChangeRole />} />
          <Route path="VenuesManagement/Create" element={<VenuesManagementCreate />} />
          <Route path="VenuesManagement/Edit" element={<VenuesManagementEdit />} />
          <Route path="VenuesManagement" element={<VenuesManagement />} />
        </Route>

        <Route element={<RequireAuth allowedRoles={[ROLES.EventManager, ROLES.Administrator]} />}>
          <Route path="EventsManagement/Edit" element={<EventsManagementEdit />} />
          <Route path="EventsManagement/Insert" element={<EventsManagementInsert />} />
          <Route path="EventsManagement/SelectLayouts" element={<EventsManagementSelectLayouts />} />
          <Route path="EventsManagement/SelectVenues" element={<EventsManagementSelectVenues />} />
          <Route path="ThirdPartyEvents/Preview" element={<ThirdPartyEvents />} />
        </Route>

        {/* catch all */}
        <Route path="*" element={<Missing />} />
      </Routes>
    </Layout>
  );
}

export default App;
