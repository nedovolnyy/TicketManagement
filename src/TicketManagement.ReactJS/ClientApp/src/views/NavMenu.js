import React from 'react';
import { Navbar, NavbarBrand, NavItem, NavLink } from 'reactstrap';
import { useTranslation } from 'react-i18next';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export default function NavMenu() {
  const { t } = useTranslation();

  return (
    <header>
      <Navbar className="navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3" container light>
        <NavbarBrand tag={Link} to="/">{t('TicketManagement')}</NavbarBrand>
          <ul className="navbar-nav flex-grow">
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/fetch-data">{t('Upload Event from file:')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/AreasManagement">{t('Areas management')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/LayoutsManagement">{t('Layouts management')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/VenuesManagement">{t('Venues management')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/UsersManagement">{t('Users management')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/cart">{t('Cart')}</NavLink>
            </NavItem>

            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/Account/Register">{t('Register')}</NavLink>
            </NavItem>
            <NavItem>
              <NavLink tag={Link} className="text-dark" to="/Account/Login">{t('Login')}</NavLink>
            </NavItem>
          </ul>
      </Navbar>
    </header>
  );
}
