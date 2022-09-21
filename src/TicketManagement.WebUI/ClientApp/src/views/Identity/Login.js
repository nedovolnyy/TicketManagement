import { useRef, useState, useEffect } from 'react'
import { useTranslation } from 'react-i18next'
import useAuth from '../../hooks/useAuth'
import { Link, useNavigate, useLocation } from 'react-router-dom'
import axios from '../../api/axios'

const LOGIN_URL = '/api/Users/Login'

const Login = () => {
  const { setAuth } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();
  const from = location.state?.from?.pathname || "/";

  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState('');
  const [pwd, setPwd] = useState('');
  const [errMsg, setErrMsg] = useState('');

  useEffect(() => {
    userRef.current.focus();
  }, [])

  useEffect(() => {
    setErrMsg('');
  }, [user, pwd])

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const response = await axios.post(LOGIN_URL,
        JSON.stringify({ email: user, password: pwd }),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        }
      );
      const accessToken = response?.data?.token;
      const roles = response?.data?.roles;
      const userResponse = response?.data?.user;
      setAuth({ user, userResponse, roles, accessToken });
      setUser('');
      setPwd('');
      navigate(from, { replace: true });
    } catch (err) {
      if (!err?.response) {
        setErrMsg('No Server Response');
      } else if (err.response?.status === 400) {
        setErrMsg('Missing Username or Password');
      } else if (err.response?.status === 401) {
        setErrMsg('Unauthorized');
      } else {
        setErrMsg('Login Failed');
      }
      errRef.current.focus();
    }
  }
  const { t } = useTranslation();

  return (
    <div className="row">
      <div className="col-md-4">
        <h2>{t('Use a local account to log in.')}</h2>
        <hr />
        <form onSubmit={handleSubmit}>
          <div className="form-floating">
            <input
              className="form-control"
              type="text"
              id="email"
              ref={userRef}
              autoComplete="off"
              onChange={(e) => setUser(e.target.value)}
              value={user}
              required
            />
            <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="email">{t('Email')}:</label>
          </div>
          <br></br>
          <div className="form-floating">
            <input
              className="form-control"
              type="password"
              id="password"
              onChange={(e) => setPwd(e.target.value)}
              value={pwd}
              required
            />
            <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="password">{t('Password')}:</label>
          </div>
          <br></br>
          <div>
            <button id="login-submit" type="submit" className="w-100 btn btn-lg btn-primary">{t('Log in')}</button>
          </div>
          <h5 ref={errRef} className={errMsg ? "errmsg" : "offscreen"} aria-live="assertive">{errMsg}</h5>
          <br></br>
          <div>
            <p>
              <Link to="/Identity/Account/Register">{t('Register as a new user')}</Link>
            </p>
          </div>
        </form>
      </div>
    </div>
  )
}

export default Login
