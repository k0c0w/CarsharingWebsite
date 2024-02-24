export const google = {
    scope: 'https://www.googleapis.com/auth/userinfo.email',
    client_id: '687415070028-3o7siun1s5skobj4bao912tebcjbp34f.apps.googleusercontent.com',
    redirect_uri: `${process.env.REACT_APP_WEBSITE_API_URL}/account/google-external-auth-callback`
}

export default google;