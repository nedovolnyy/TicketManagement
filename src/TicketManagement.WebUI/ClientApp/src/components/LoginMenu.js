import React, { Fragment, useContext } from 'react'
import { NavItem, NavLink } from 'reactstrap'
import { useNavigate, Link } from 'react-router-dom'
import { withTranslation } from 'react-i18next'
import AuthContext from "../context/AuthProvider";
import useAuth from '../hooks/useAuth';

function LoginMenuPlain(props) {
  const { t } = props;
  const { auth } = useAuth();
  const { setAuth } = useContext(AuthContext);
  const navigate = useNavigate();

  const logout = async () => {
    setAuth({});
  }

  if (!auth?.user) {
    const registerPath = 'Identity/Account/Register';
    const loginPath = 'Identity/Account/Login';
    return (<Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={registerPath}>{t('Register')}</NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={loginPath}>{t('Login')}</NavLink>
      </NavItem>
    </Fragment>);
  } else {
    const logoutPath = { pathname: 'Identity/Account/Logout', state: { local: true } };
    return (<Fragment>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to='/'>
          {t('Hello')}, {auth?.user}
        </NavLink>
      </NavItem>
      <NavItem>
        <NavLink tag={Link} className="text-dark" to={logoutPath} onClick={logout}>
          {t('Logout')}
        </NavLink>
      </NavItem>
    </Fragment >);
  }
}

export const LoginMenu = withTranslation()(LoginMenuPlain);
