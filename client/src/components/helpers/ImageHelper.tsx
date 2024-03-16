
const getUserImage = (image?: string) => {
  return (
    process.env.REACT_APP_SERVER_URI + `/images/users/${image}`
  )
}

export default getUserImage