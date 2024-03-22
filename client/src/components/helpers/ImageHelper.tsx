
const getUserImage = (image?: string) => {
  return (
    process.env.REACT_APP_API_URI + `/images/users/${image}`
  )
}

export default getUserImage