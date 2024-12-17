import React from "react";
import { Container, Box, Typography, TextField, Button, Alert } from "@mui/material";
import { Formik, Form, Field } from "formik";
import * as Yup from "yup";
import api from "../../api/api";
import { useDispatch } from "react-redux";
import { useLocation, useNavigate } from "react-router-dom";
import { setUser } from "../../redux/userSlice";
import { setAuthenticated } from "../../redux/authSlice";
import { jwtDecode } from "jwt-decode";

const LoginSchema = Yup.object().shape({
  email: Yup.string()
    .email("Invalid email address")
    .required("Email is required"),
  password: Yup.string()
    .min(6, "Password must be at least 6 characters")
    .required("Password is required"),
});

const Login = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();
  const location = useLocation();

  const from = location.state?.from?.pathname || "/";

  const handleLogin = async (values, { setSubmitting, setErrors }) => {
    try {
      const response = await api.post("/account/login", {
        email: values.email,
        password: values.password,
      });
      const { accessToken } = response.data;

      const decodedToken = jwtDecode(accessToken);
      const user = {
        userId: decodedToken.sub,
        userEmail: decodedToken.email,
        carListings: decodedToken.CarListings,
      };

      localStorage.setItem("token", accessToken);

      dispatch(setAuthenticated(true));
      dispatch(setUser(user));
      navigate(from, { replace: true });
    } catch (err) {
      if (err.response && err.response.data) {
        setErrors({ general: err.response.data.message || "Invalid email or password." });
      } else {
        setErrors({ general: "Something went wrong. Please try again." });
      }
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Container maxWidth="xs">
      <Box
        sx={{
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
          marginTop: 8,
          padding: 3,
          borderRadius: 2,
          boxShadow: 2,
          bgcolor: "background.paper",
        }}
      >
        <Typography variant="h4" sx={{ mb: 2 }}>
          Sign In
        </Typography>

        <Formik
          initialValues={{ email: "", password: "" }}
          validationSchema={LoginSchema}
          onSubmit={handleLogin}
        >
          {({ errors, touched, isSubmitting }) => (
            <Form style={{ width: "100%" }}>
              {errors.general && (
                <Alert severity="error" sx={{ mb: 2 }}>
                  {errors.general}
                </Alert>
              )}

              <Field
                as={TextField}
                label="Email"
                name="email"
                type="email"
                fullWidth
                required
                error={touched.email && Boolean(errors.email)}
                helperText={touched.email && errors.email}
                sx={{ mb: 2 }}
              />

              <Field
                as={TextField}
                label="Password"
                name="password"
                type="password"
                fullWidth
                required
                error={touched.password && Boolean(errors.password)}
                helperText={touched.password && errors.password}
                sx={{ mb: 2 }}
              />

              <Button
                type="submit"
                variant="contained"
                color="primary"
                fullWidth
                disabled={isSubmitting}
              >
                {isSubmitting ? "Signing In..." : "Sign In"}
              </Button>
            </Form>
          )}
        </Formik>

        <Typography variant="body2" sx={{ mt: 2 }}>
          Don't have an account?
          <Button variant="text" color="primary">
            Register
          </Button>
        </Typography>
      </Box>
    </Container>
  );
};

export default Login;