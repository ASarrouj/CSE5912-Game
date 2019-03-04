using UnityEngine;

namespace AI {
    public class PredictiveAim
    {
        static float PredictiveAimWildGuessAtImpactTime() {
            return Random.Range(1, 5);
        }

        static public bool PredictAim(Vector3 muzzlePosition, float projectileSpeed, Vector3 targetPosition, Vector3 targetVelocity, float gravity, out Vector3 projectileVelocity) {
            if (muzzlePosition == targetPosition) {
                projectileVelocity = projectileSpeed * (Random.rotation * Vector3.forward);
                return true;
            }

            float projectileSpeedSq = projectileSpeed * projectileSpeed;
            float targetSpeedSq = targetVelocity.sqrMagnitude;
            float targetSpeed = Mathf.Sqrt(targetSpeedSq);
            Vector3 targetToMuzzle = muzzlePosition - targetPosition;
            float targetToMuzzleDistSq = targetToMuzzle.sqrMagnitude;
            float targetToMuzzleDist = Mathf.Sqrt(targetToMuzzleDistSq);
            Vector3 targetToMuzzleDir = targetToMuzzle;
            targetToMuzzleDir.Normalize();

            Vector3 targetVelocityDir = targetVelocity;
            targetVelocityDir.Normalize();

            float cosTheta = Vector3.Dot(targetToMuzzleDir, targetVelocityDir);

            bool validSolutionFound = true;
            float t;
            if (Mathf.Approximately(projectileSpeedSq, targetSpeedSq)) {
                if (cosTheta > 0) {
                    t = 0.5f * targetToMuzzleDist / (targetSpeed * cosTheta);
                } else {
                    validSolutionFound = false;
                    t = PredictiveAimWildGuessAtImpactTime();
                }
            } else {
                float a = projectileSpeedSq - targetSpeedSq;
                float b = 2.0f * targetToMuzzleDist * targetSpeed * cosTheta;
                float c = -targetToMuzzleDistSq;
                float discriminant = b * b - 4.0f * a * c;

                if (discriminant < 0) {
                    validSolutionFound = false;
                    t = PredictiveAimWildGuessAtImpactTime();
                } else {
                    float uglyNumber = Mathf.Sqrt(discriminant);
                    float t0 = 0.5f * (-b + uglyNumber) / a;
                    float t1 = 0.5f * (-b - uglyNumber) / a;
                    t = Mathf.Min(t0, t1);
                    if (t < Mathf.Epsilon) {
                        t = Mathf.Max(t0, t1);
                    }

                    if (t < Mathf.Epsilon) {
                        validSolutionFound = false;
                        t = PredictiveAimWildGuessAtImpactTime();
                    }
                }
            }

            projectileVelocity = targetVelocity + (-targetToMuzzle / t);
            if (!validSolutionFound) {
                projectileVelocity = projectileSpeed * projectileVelocity.normalized;
            }

            if (!Mathf.Approximately(gravity, 0)) {
                Vector3 projectileAcceleration = gravity * Vector3.down;
                Vector3 gravityCompensation = (0.5f * projectileAcceleration * t);
                float gravityCompensationCap = 0.5f * projectileSpeed;
                if (gravityCompensation.magnitude > gravityCompensationCap) {
                    gravityCompensation = gravityCompensationCap * gravityCompensation.normalized;
                }
                projectileVelocity -= gravityCompensation;
            }

            return validSolutionFound;
        }
    }
}