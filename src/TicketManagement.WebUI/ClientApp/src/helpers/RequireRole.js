import useAuth from "../hooks/useAuth";

const RequireRole = ({ allowedRoles }) => {
  const { auth } = useAuth();

  return (
    auth?.roles?.find(role => allowedRoles?.includes(role))
      ? true
      : false
  );
}

export default RequireRole;
