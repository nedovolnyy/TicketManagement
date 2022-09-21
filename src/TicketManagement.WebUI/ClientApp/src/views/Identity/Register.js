import { useRef, useState, useEffect, Fragment } from 'react'
import { faCheck, faTimes, faInfoCircle } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { useTranslation } from 'react-i18next'
import axios from '../../api/axios'
import { Link, useNavigate } from 'react-router-dom'
import useAuth from '../../hooks/useAuth'

const EMAIL_REGEX = /^(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9]))\.){3}(?:(2(5[0-5]|[0-4][0-9])|1[0-9][0-9]|[1-9]?[0-9])|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])/;
const PWD_REGEX = /^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[!@#$%]).{8,24}$/;
const REGISTER_URL = '/api/Users/Register';

const Register = () => {
  const { t } = useTranslation();
  const { setAuth } = useAuth();
  const navigate = useNavigate();
  const userRef = useRef();
  const errRef = useRef();

  const [user, setUser] = useState('');
  const [validName, setValidName] = useState(false);
  const [userFocus, setUserFocus] = useState(false);

  const [pwd, setPwd] = useState('');
  const [validPwd, setValidPwd] = useState(false);
  const [pwdFocus, setPwdFocus] = useState(false);

  const [matchPwd, setMatchPwd] = useState('');
  const [validMatch, setValidMatch] = useState(false);
  const [matchFocus, setMatchFocus] = useState(false);

  const [errMsg, setErrMsg] = useState('');
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    userRef.current.focus();
  }, [])

  useEffect(() => {
    setValidName(EMAIL_REGEX.test(user));
  }, [user])

  useEffect(() => {
    setValidPwd(PWD_REGEX.test(pwd));
    setValidMatch(pwd === matchPwd);
  }, [pwd, matchPwd])

  useEffect(() => {
    setErrMsg('');
  }, [user, pwd, matchPwd])

  const handleSubmit = async (e) => {
    e.preventDefault();
    const v1 = EMAIL_REGEX.test(user);
    const v2 = PWD_REGEX.test(pwd);
    if (!v1 || !v2) {
      setErrMsg("Invalid Entry");
      return;
    }
    try {
      const response = await axios.post(REGISTER_URL,
        JSON.stringify({ email: user, password: pwd, confirmPassword: matchPwd }),
        {
          headers: { 'Content-Type': 'application/json' },
          withCredentials: true
        }
      );
      setSuccess(true);
      const accessToken = response?.data?.token;
      const roles = response?.data?.roles;
      const userResponse = response?.data?.user;
      setAuth({ user, userResponse, roles, accessToken });
      setUser('');
      setPwd('');
      setMatchPwd('');
      navigate('/', { replace: true });
    } catch (err) {
      if (!err?.response) {
        setErrMsg('No Server Response');
      } else if (err.response?.status === 409) {
        setErrMsg('Username Taken');
      } else {
        setErrMsg('Registration Failed')
      }
      errRef.current.focus();
    }
  }

  return (
    <Fragment>
      {success ? (
        <section>
          <h1>{t('Success')}!</h1>
          <p>
            <Link href="#">{t('Sign In')}</Link>
          </p>
        </section>
      ) : (
        <section>
          <div className="row">
            <div className="col-md-4">
              <h2>{t('Create a new account.')}</h2>
              <hr />
              <form onSubmit={handleSubmit}>
                <div className="form-floating">
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="email">
                    {t('Email')}:
                    <FontAwesomeIcon icon={faCheck} className={validName ? "d-block" : "d-none"} />
                    <FontAwesomeIcon icon={faTimes} className={validName || !user ? "d-none" : "d-block"} />
                  </label>
                  <input
                    className="form-control"
                    type="text"
                    id="email"
                    ref={userRef}
                    autoComplete="off"
                    onChange={(e) => setUser(e.target.value)}
                    value={user}
                    required
                    aria-invalid={validName ? "false" : "true"}
                    aria-describedby="uidnote"
                    onFocus={() => setUserFocus(true)}
                    onBlur={() => setUserFocus(false)}
                  />
                  <br></br>
                  <p id="uidnote" className={userFocus && user && !validName ? "d-block" : "d-none"}>
                    <FontAwesomeIcon icon={faInfoCircle} />
                    4 to 24 characters.<br />
                    Must begin with a letter.<br />
                    Letters, numbers, underscores, hyphens allowed.
                  </p>
                </div>
                <br></br>
                <div className="form-floating">
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="password">
                    {t('Password')}:
                    <FontAwesomeIcon icon={faCheck} className={validPwd ? "d-block" : "d-none"} />
                    <FontAwesomeIcon icon={faTimes} className={validPwd || !pwd ? "d-none" : "d-block"} />
                  </label>
                  <input
                    className="form-control"
                    type="password"
                    id="password"
                    onChange={(e) => setPwd(e.target.value)}
                    value={pwd}
                    required
                    aria-invalid={validPwd ? "false" : "true"}
                    aria-describedby="pwdnote"
                    onFocus={() => setPwdFocus(true)}
                    onBlur={() => setPwdFocus(false)}
                  />
                  <br></br>
                  <p id="pwdnote" className={pwdFocus && !validPwd ? "d-block" : "d-none"}>
                    <FontAwesomeIcon icon={faInfoCircle} />
                    8 to 24 characters.<br />
                    Must include uppercase and lowercase letters, a number and a special character.<br />
                    Allowed special characters: <span aria-label="exclamation mark">!</span> <span aria-label="at symbol">@</span> <span aria-label="hashtag">#</span> <span aria-label="dollar sign">$</span> <span aria-label="percent">%</span>
                  </p>
                </div>
                <br></br>
                <div className="form-floating">
                  <label className="col-sm-2 col-form-label col-form-label-sm text-muted" htmlFor="confirm_pwd">
                    {t('Confirm Password')}:
                    <FontAwesomeIcon icon={faCheck} className={validMatch && matchPwd ? "d-block" : "d-none"} />
                    <FontAwesomeIcon icon={faTimes} className={validMatch || !matchPwd ? "d-none" : "d-block"} />
                  </label>
                  <input
                    className="form-control"
                    type="password"
                    id="confirm_pwd"
                    onChange={(e) => setMatchPwd(e.target.value)}
                    value={matchPwd}
                    required
                    aria-invalid={validMatch ? "false" : "true"}
                    aria-describedby="confirmnote"
                    onFocus={() => setMatchFocus(true)}
                    onBlur={() => setMatchFocus(false)}
                  />
                  <br></br>
                  <p id="confirmnote" className={matchFocus && !validMatch ? "d-block" : "d-none"}>
                    <FontAwesomeIcon icon={faInfoCircle} />
                    Must match the first password input field.
                  </p>
                </div>
                <br></br>
                <button className="w-100 btn btn-lg btn-primary" disabled={!validName || !validPwd || !validMatch ? true : false}>{t('Register')}</button>
              </form>
              <h5 ref={errRef} className={errMsg ? "errmsg" : "d-none"} aria-live="assertive">{errMsg}</h5>
              <p>
                <span className="line">
                  <Link to="/Identity/Account/Login">{t('Sign In')}</Link>
                </span>
              </p>
            </div>
          </div>
        </section>
      )}
    </Fragment>
  )
}

export default Register
