<template>
  <div class="bg-light min-vh-100 d-flex flex-row align-items-center">
    <CContainer>
      <CRow class="justify-content-center">
        <CCol :md="8">
          <CCardGroup>
            <CCard class="p-4">
              <CCardBody>
                <CForm>
                  <h1>Login</h1>
                  <p class="text-medium-emphasis">Sign In to your account</p>
                  <CInputGroup class="mb-3">
                    <CInputGroupText>
                      <CIcon icon="cil-user" />
                    </CInputGroupText>
                    <CFormInput
                      placeholder="Username"
                      autocomplete="username"
                      v-model="model.email"
                    />
                  </CInputGroup>
                  <CInputGroup class="mb-4">
                    <CInputGroupText>
                      <CIcon icon="cil-lock-locked" />
                    </CInputGroupText>
                    <CFormInput
                      type="password"
                      placeholder="Password"
                      v-model="model.password"
                      autocomplete="current-password"
                    />
                  </CInputGroup>
                  <CRow>
                    <CCol :xs="6">
                      <CButton color="primary" class="px-4" @click="submit">
                        Login
                      </CButton>
                    </CCol>
                    <CCol :xs="6" class="text-right">
                      <CButton color="link" class="px-0">
                        Forgot password?
                      </CButton>
                    </CCol>
                  </CRow>
                </CForm>
              </CCardBody>
            </CCard>
  
          </CCardGroup>
        </CCol>
      </CRow>
    </CContainer>
  </div>
</template>

<script>
import axios from 'axios'
export default {
  name: 'Login',
  data: function () {
    return {
      model: {
        email: 'admin@admin.com',
        password: 'admin',
      },
    }
  },
  methods: {
    submit: function () {
      let base = this
      axios.post('/Account', base.model).then((response) => {
        if (response.data.status == 200) {
          alert("Login successfully")
          let user = response.data.obj
          localStorage.setItem('Id', user.id)
          localStorage.setItem('email', user.email)
          localStorage.setItem('firstname', user.firstname)
          localStorage.setItem('lastname', user.lastname)
          localStorage.setItem('username', user.username)
          localStorage.setItem('userRoleId', user.userRoleId)
          localStorage.setItem('userRole', user.userRole)
          localStorage.setItem('token', user.token)

          location.href = '/dashboard#/dashboard'
        } else {
          alert(response.data.obj)
        }
        console.log(response.data)
      })
    },
  },
}
</script>
