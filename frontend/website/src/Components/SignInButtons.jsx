import React from 'react'
import '../css/google-login-button.css'

export default function GoogleSignIn ({redirect_uri, client_id, scope}) {

  var url = `https://accounts.google.com/o/oauth2/auth?client_id=${client_id}&response_type=code&redirect_uri=${redirect_uri}&scope=${scope}`

  return (
    <a href={url}>
      <div class='google-btn'>
        <div class='google-icon-wrapper'>
          <img
            class='google-icon'
            src='https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg'
          />
        </div>
        <p class='btn-text'>
          <b>Sign in with google</b>
        </p>
      </div>
    </a>
  )
}
