import React, { useState } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink, Form, Label, Input } from 'reactstrap';
import { withTranslation } from 'react-i18next';
import { LoginMenu } from '../components/LoginMenu';
import { Link, useNavigate } from 'react-router-dom';
import { ROLES } from '../App';
import './NavMenu.css';
import RequireRole from '../helpers/RequireRole';
import { DataNavigation } from 'react-data-navigation';

function NavMenuPlain(props) {
  const { t } = props;
  const [collapsed, setCollapsed] = useState(true);
  const navigate = useNavigate();
  const toggleNavbar = () => {
    setCollapsed(!collapsed);
  }

  const handleSelectFile = (event) => {
    const reader = new FileReader();
    reader.onload = (event) => {};
    reader.readAsText(event.target.files[0]);
    DataNavigation.setData('ThirdPartyEvents', reader);
    navigate("ThirdPartyEvents/Preview");
  }

  return (
    <header>
      <Navbar className='navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3' light>
        <NavbarBrand tag={Link} to='/'>TicketManagement</NavbarBrand>
        <NavbarToggler onClick={toggleNavbar} className='mr-2' />
        <Collapse className='d-sm-inline-flex flex-sm-row-reverse' isOpen={!collapsed} navbar>
          <ul className='navbar-nav flex-grow'>
            {RequireRole({ allowedRoles: [ROLES.Administrator, ROLES.EventManager] }) &&
              <>
                <li className='nav-item e-upload e-control-wrapper e-lib e-keyboard'>
                  <form /*'ThirdPartyEvents/Preview'*/ method='post' encType='multipart/form-data'>
                    <Label>{t('Upload Event from file:')}</Label>
                    <Input type='file' name='file' accept='.json' onChange={handleSelectFile} className='inputFileStyle' /> {/* todo */}
                  </form>
                </li>
                <NavItem className='nav-item'>
                  <NavLink tag={Link} className='text-dark' to='/EventsManagement/SelectVenues'>{t('Create event')}</NavLink>
                </NavItem>
              </>
            }
            {RequireRole({ allowedRoles: [ROLES.Administrator] }) &&
              <>
                <NavItem>
                  <NavLink tag={Link} className='text-dark' to='/AreasManagement'>{t('Areas management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className='text-dark' to='/LayoutsManagement'>{t('Layouts management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className='text-dark' to='/VenuesManagement'>{t('Venues management')}</NavLink>
                </NavItem>
                <NavItem>
                  <NavLink tag={Link} className='text-dark' to='/UsersManagement'>{t('Users management')}</NavLink>
                </NavItem>
              </>
            }
            {RequireRole({ allowedRoles: [ROLES.Administrator, ROLES.EventManager, ROLES.User] }) &&
              <NavItem>
                <NavLink tag={Link} className='text-dark' to='/cart'>{t('Cart')}</NavLink>
              </NavItem>
            }
            <LoginMenu />
          </ul>
        </Collapse>
      </Navbar>
    </header>
  );
}

export const NavMenu = withTranslation()(NavMenuPlain);
