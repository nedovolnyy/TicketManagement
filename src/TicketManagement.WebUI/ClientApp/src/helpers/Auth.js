import useAuth from "../hooks/useAuth";

export const Auth = ( Component ) => {
  function ComponentWithAuthProp(props) {
    let { auth } = useAuth();
    return (
      <Component
        {...props}
        auth={auth}
      />
    );
  }
  return ComponentWithAuthProp;
}
