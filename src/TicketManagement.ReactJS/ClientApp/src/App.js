import React from 'react';
import { Route, Routes } from 'react-router-dom';
import { Layout } from './views/Layout';
import './custom.css';
import RequireAuth from './components/RequireAuth';
import Register from './views/Identity/Register';
import Login from './views/Identity/Login';
import Missing from './views/Identity/Missing';
import Unauthorized from './views/Identity/Unauthorized';
import Cart from "./views/Home/Cart";
import NoBalance from "./views/Home/NoBalance";
import { Home } from "./views/Home/Home";

const ROLES = {
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
        <Route path="Identity/Account/Register" element={<Register />} />
        <Route path="Identity/Account/Login" element={<Login />} />
        <Route path="unauthorized" element={<Unauthorized />} />

        {/* we want to protect these routes */}
        <Route element={<RequireAuth allowedRoles={[ROLES.User, ROLES.Administratormin, ROLES.EventManager]} />}>
          <Route path="cart" element={<Cart />} />
          <Route path="nobalance" element={<NoBalance />} />
        </Route>
        {/*
          <Route element={<RequireAuth allowedRoles={[ROLES.EventManager]} />}>
            <Route path="editor" element={<Editor />} />
          </Route>

          <Route element={<RequireAuth allowedRoles={[ROLES.Administratormin]} />}>
            <Route path="admin" element={<Admin />} />
          </Route>

          <Route element={<RequireAuth allowedRoles={[ROLES.EventManager, ROLES.Administratormin]} />}>
            <Route path="lounge" element={<Lounge />} />
          </Route>

          {/* catch all */}
        <Route path="*" element={<Missing />} />
      </Routes>
    </Layout>
  );
}

export default App;
