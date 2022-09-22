import { Link } from "react-router-dom"

const Logout = () => {
    return (
        <article style={{ padding: "100px" }}>
            <h1>Success!</h1>
            <p>User logged out.</p>
            <div className="flexGrow">
                <Link to="/">Visit Our Homepage</Link>
            </div>
        </article>
    )
}

export default Logout
